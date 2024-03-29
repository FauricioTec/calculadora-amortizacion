﻿using System.Globalization;
using System.Xml;

namespace Model;

public class ClienteBccrWs
{
    private readonly HttpClient cliente;
    private readonly string email;
    private readonly string nombre;
    private readonly string token;

    public ClienteBccrWs(string nombre, string email, string token)
    {
        this.nombre = nombre;
        this.email = email;
        this.token = token;
        cliente = new HttpClient();
    }

    public async Task<double> ObtenerTipoCambioDolar()
    {
        string url =
            "https://gee.bccr.fi.cr/Indicadores/Suscripciones/WS/wsindicadoreseconomicos.asmx/ObtenerIndicadoresEconomicos?Indicador=317&FechaInicio=" +
            DateTime.Now.ToString("dd/MM/yyyy") + "&FechaFinal=" + DateTime.Now.ToString("dd/MM/yyyy") +
            "&Nombre=" + nombre + "&SubNiveles=N&CorreoElectronico=" + email + "&Token=" + token;
        string respuesta = await GetAsync(url);
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(respuesta);
        XmlNodeList valor = xml.GetElementsByTagName("NUM_VALOR");
        string valorStr = valor[0].InnerText.Trim();
        return double.Parse(valorStr, CultureInfo.InvariantCulture.NumberFormat);
    }

    private async Task<string> GetAsync(string uri)
    {
        using var response = await cliente.GetAsync(uri);
        return await response.Content.ReadAsStringAsync();
    }
}