// See https://aka.ms/new-console-template for more information
using Model;

Console.WriteLine("Hello, World!");
ClienteBccrWs clienteBccrWs = new ClienteBccrWs("Fauricio Granados", "fauricio.gr@gmail.com", "RRS0O0A886");
Console.WriteLine("Llega aqui 4");
double tipoCambioDolar = await clienteBccrWs.ObtenerTipoCambioDolar();
Console.WriteLine("Llega aqui 5");
Console.WriteLine(tipoCambioDolar);