public static class HelperDeArchivos
{
    public static string AbrirArchivoJson(string RutaDelArchivoJson)
    {
        string TextoLeido;
        using (var FS = new FileStream(RutaDelArchivoJson, FileMode.Open))
        {
            using (var SR = new StreamReader(FS))
            {
                TextoLeido = SR.ReadLine();
                FS.Close();
            }
        }
        return TextoLeido;
    }
    public static void GuardarArchivoJson(string RutaDelArchivoJson, string TextoAGuardar)
    {
        using (var FS = new FileStream(RutaDelArchivoJson, FileMode.OpenOrCreate))
        {
            using (var SW = new StreamWriter(FS))
            {
                SW.WriteLine(TextoAGuardar);
                SW.Close();
            }
        }
    }
    public static string AbrirArchivoCsv(string RutaDelArchivoCsv)
    {
        string TextoLeido;
        using (var FS = new FileStream(RutaDelArchivoCsv, FileMode.Open))
        {
            using (var SR = new StreamReader(FS))
            {
                TextoLeido = SR.ReadToEnd();
                FS.Close();
            }
        }
        return TextoLeido;
    }
    public static void GuardarArchivoCsv(string RutaDelArchivoCsv, string TextoAGuardar)
    {
        using (TextWriter TW = File.AppendText(RutaDelArchivoCsv))
        {
            TW.WriteLine(TextoAGuardar);
            TW.Close();
        }
    }
}