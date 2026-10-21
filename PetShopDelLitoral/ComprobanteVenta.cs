using Capa_Negocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace PetShopDelLitoral
{
    // Genera el comprobante PDF de una venta YA registrada, las veces que haga falta.
    // Lo usan los reportes (administrador y vendedor). Solo LEE de la base y de la fila de la
    // grilla: no toca stock, caja ni el estado de la venta.
    internal static class ComprobanteVenta
    {
        private static readonly CultureInfo cultura = new CultureInfo("es-AR");

        // Agrega (si no existe) la columna de botón "Comprobante" al final de la grilla.
        public static void AsegurarColumna(DataGridView dgv)
        {
            DataGridViewButtonColumn col = dgv.Columns["columnaComprobante"] as DataGridViewButtonColumn;
            if (col == null)
            {
                col = new DataGridViewButtonColumn();
                col.Name = "columnaComprobante";
                col.HeaderText = "Comprobante";
                col.Text = "Comprobante";
                col.UseColumnTextForButtonValue = true;
                col.MinimumWidth = 6;
                col.FlatStyle = FlatStyle.Flat;
                dgv.Columns.Add(col);
            }
            col.DisplayIndex = dgv.Columns.Count - 1;
        }

        public static bool EsColumnaComprobante(DataGridView dgv, int columnIndex)
        {
            return columnIndex >= 0 && dgv.Columns[columnIndex].Name == "columnaComprobante";
        }

        // Punto de entrada: se llama con el clic en el botón de la fila.
        public static void Generar(IWin32Window owner, DataGridView dgv, int rowIndex)
        {
            try
            {
                DataRowView drv = dgv.Rows[rowIndex].DataBoundItem as DataRowView;
                if (drv == null) return;
                DataRow cab = drv.Row;

                // El id es la columna "N° Venta" (o la primera del DataTable)
                DataColumn colId = cab.Table.Columns[0];
                foreach (DataColumn c in cab.Table.Columns)
                {
                    string n = (c.ColumnName ?? "").Trim();
                    if (n.StartsWith("N", StringComparison.OrdinalIgnoreCase) &&
                        n.EndsWith("Venta", StringComparison.OrdinalIgnoreCase)) { colId = c; break; }
                }
                int idVenta = Convert.ToInt32(cab[colId]);

                DataTable det = new CN_Reporte().ObtenerDetalleVenta(idVenta);
                if (det == null || det.Rows.Count == 0)
                {
                    MessageBox.Show("La venta N° " + idVenta + " no tiene detalle para imprimir.", "Comprobante",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Archivo PDF (*.pdf)|*.pdf";
                    sfd.FileName = "Comprobante_Venta_" + idVenta.ToString("D8") + ".pdf";
                    if (sfd.ShowDialog(owner) != DialogResult.OK) return;

                    try
                    {
                        EscribirPdf(sfd.FileName, idVenta, cab, det);
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("No se pudo guardar el PDF. Si ya lo tenés abierto, cerralo y volvé a intentar.",
                                        "Comprobante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el comprobante: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Primer valor de la fila cuya columna contenga alguna de las palabras clave
        private static string BuscarValor(DataRow fila, params string[] claves)
        {
            foreach (string k in claves)
                foreach (DataColumn c in fila.Table.Columns)
                    if (c.ColumnName.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0 && !(fila[c] is DBNull))
                        return Convert.ToString(fila[c]);
            return "";
        }

        private static string BuscarColumna(DataTable t, params string[] claves)
        {
            foreach (string k in claves)
                foreach (DataColumn c in t.Columns)
                    if (c.ColumnName.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0) return c.ColumnName;
            return null;
        }

        private static decimal ADecimal(object v)
        {
            if (v == null || v is DBNull) return 0m;
            if (v is decimal) return (decimal)v;
            string s = Convert.ToString(v).Replace("$", "").Replace("%", "").Trim();
            decimal d;
            if (decimal.TryParse(s, NumberStyles.Number, cultura, out d)) return d;
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out d)) return d;
            return 0m;
        }

        private static void EscribirPdf(string ruta, int idVenta, DataRow cab, DataTable det)
        {
            var fTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.DARK_GRAY);
            var fSub = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.GRAY);
            var fNeg = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.BLACK);
            var fNor = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
            var fDup = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.RED);
            string linea = "------------------------------------------------------------------------------------------------------------------";

            using (FileStream fs = new FileStream(ruta, FileMode.Create))
            {
                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter.GetInstance(doc, fs);
                doc.Open();

                doc.Add(new Paragraph("PET SHOP DEL LITORAL", fTitulo));
                doc.Add(new Paragraph("Sistema de Gestión Integral", fSub));
                doc.Add(new Paragraph(linea, fSub));
                doc.Add(new Paragraph("Comprobante de Venta N°: " + idVenta.ToString("D8"), fNeg));
                doc.Add(new Paragraph("DUPLICADO - reimpreso el " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fDup));

                string fecha = BuscarValor(cab, "fecha");
                DateTime f;
                if (DateTime.TryParse(fecha, cultura, DateTimeStyles.None, out f)) fecha = f.ToShortDateString();
                if (fecha != "") doc.Add(new Paragraph("Fecha: " + fecha, fNor));

                string vend = BuscarValor(cab, "vendedor", "usuario");
                string cli = BuscarValor(cab, "cliente");
                string pago = BuscarValor(cab, "pago", "metodo", "método");
                if (vend != "") doc.Add(new Paragraph("Vendedor: " + vend, fNor));
                if (cli != "") doc.Add(new Paragraph("Cliente: " + cli, fNor));
                if (pago != "") doc.Add(new Paragraph("Método de pago: " + pago, fNor));

                doc.Add(new Paragraph(linea, fSub));
                doc.Add(new Chunk(Chunk.NEWLINE));

                // Columnas del detalle identificadas por nombre; las que no aparecen se omiten
                string cCant = BuscarColumna(det, "cant");
                string cProd = BuscarColumna(det, "producto", "nombre", "descrip");
                string cPrec = BuscarColumna(det, "precio");
                string cDesc = BuscarColumna(det, "desc");
                string cSub = BuscarColumna(det, "subtotal");

                List<string[]> cols = new List<string[]>();   // { título, columna, ancho }
                if (cCant != null) cols.Add(new[] { "Cant", cCant, "12" });
                if (cProd != null) cols.Add(new[] { "Producto", cProd, "38" });
                if (cPrec != null) cols.Add(new[] { "Precio U.", cPrec, "18" });
                if (cDesc != null) cols.Add(new[] { "Desc %", cDesc, "14" });
                if (cSub != null) cols.Add(new[] { "Subtotal", cSub, "18" });

                decimal subtotal = 0m;
                if (cols.Count >= 2)
                {
                    PdfPTable tabla = new PdfPTable(cols.Count);
                    tabla.WidthPercentage = 100;
                    float[] anchos = new float[cols.Count];
                    for (int i = 0; i < cols.Count; i++) anchos[i] = float.Parse(cols[i][2], CultureInfo.InvariantCulture);
                    tabla.SetWidths(anchos);

                    foreach (string[] c in cols)
                        tabla.AddCell(new PdfPCell(new Phrase(c[0], fNeg)) { BackgroundColor = BaseColor.LIGHT_GRAY });

                    foreach (DataRow r in det.Rows)
                    {
                        foreach (string[] c in cols)
                        {
                            string txt;
                            if (c[1] == cPrec || c[1] == cSub) txt = "$ " + ADecimal(r[c[1]]).ToString("N2", cultura);
                            else if (c[1] == cDesc) txt = ADecimal(r[c[1]]).ToString("0.##", cultura) + "%";
                            else txt = Convert.ToString(r[c[1]]);
                            tabla.AddCell(new PdfPCell(new Phrase(txt, fNor)));
                        }
                        if (cSub != null) subtotal += ADecimal(r[cSub]);
                    }
                    doc.Add(tabla);
                }
                else
                {
                    // Plan B: no se reconoció el formato del detalle, se vuelcan todas sus columnas tal cual
                    PdfPTable tabla = new PdfPTable(det.Columns.Count);
                    tabla.WidthPercentage = 100;
                    foreach (DataColumn c in det.Columns)
                        tabla.AddCell(new PdfPCell(new Phrase(c.ColumnName, fNeg)) { BackgroundColor = BaseColor.LIGHT_GRAY });
                    foreach (DataRow r in det.Rows)
                        foreach (DataColumn c in det.Columns)
                            tabla.AddCell(new PdfPCell(new Phrase(Convert.ToString(r[c]), fNor)));
                    doc.Add(tabla);
                }

                doc.Add(new Chunk(Chunk.NEWLINE));

                // El total sale de la venta guardada (columna Total de la grilla), no se recalcula
                string totalTxt = BuscarValor(cab, "total");
                decimal total = totalTxt != "" ? ADecimal(totalTxt) : subtotal;

                if (subtotal > 0 && total > 0 && subtotal != total)
                {
                    Paragraph pS = new Paragraph("Subtotal: $ " + subtotal.ToString("N2", cultura), fNor);
                    pS.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(pS);
                    Paragraph pD = new Paragraph("Descuento: - $ " + (subtotal - total).ToString("N2", cultura), fNor);
                    pD.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(pD);
                }

                Paragraph pT = new Paragraph("TOTAL GENERAL: $ " + total.ToString("N2", cultura), fTitulo);
                pT.Alignment = Element.ALIGN_RIGHT;
                doc.Add(pT);

                doc.Add(new Chunk(Chunk.NEWLINE));
                Paragraph pie = new Paragraph("¡Gracias por confiar en Pet Shop del Litoral!", fSub);
                pie.Alignment = Element.ALIGN_CENTER;
                doc.Add(pie);

                doc.Close();
            }
        }
    }
}
