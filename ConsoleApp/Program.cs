// See https://aka.ms/new-console-template for more information
using Model;
namespace ConsoleApp;

public static class Program
{
    public async static Task Main(string[] args)
    {
        Console.WriteLine("Ingrese los siguientes datos:");
        Console.Write("Nombre del cliente: ");
        string nombreCliente = Console.ReadLine();
        Console.Write("Monto del préstamo otorgado: ");
        double monto = double.Parse(Console.ReadLine());
        Console.Write("Períodos totales (plazo del préstamo) en años: ");
        int plazo = int.Parse(Console.ReadLine());
        Console.Write("Interés anual (%): ");
        double porcentajeInteresAnual = double.Parse(Console.ReadLine());
        Console.Write("Sistema de amortización deseado [\"aleman\"| \"frances\" |\"americano\"]: ");
        string sistemaAmortizacion = Console.ReadLine();
        Console.Write("Moneda que se debe usar en la tabla de amortización [c (colones) | * (dólares)]: ");
        string moneda = Console.ReadLine();

        Prestamo prestamo = new Prestamo(nombreCliente, monto, plazo, porcentajeInteresAnual);

        SistemaAmortizacion sistema = CrearSistemaAmortizacion(sistemaAmortizacion);
        ClienteBccrWs clienteBccrWs = new ClienteBccrWs("Fauricio Granados", "fauricio.gr@gmail.com", "RRS0O0A886");
        double tipoCambioDolar = await clienteBccrWs.ObtenerTipoCambioDolar();
        double tipoCambio = moneda == "c" ? 1 : tipoCambioDolar;
        TablaAmortizacion tablaAmortizacion = sistema.CrearTablaAmortizacion(prestamo, tipoCambio);
        

        // Formatear e imprimir la información
        MostrarInformacion(tablaAmortizacion, nombreCliente, monto, plazo, porcentajeInteresAnual, sistemaAmortizacion, tipoCambioDolar);
    }

    private static SistemaAmortizacion CrearSistemaAmortizacion(string sistemaAmortizacion)
    {
        switch (sistemaAmortizacion.ToLower())
        {
            case "frances":
                return new SistemaFrances();
            case "aleman":
                return new SistemaAleman();
            case "americano":
                return new SistemaAmericano();
            default:
                throw new ArgumentException("Sistema de amortización no válido");
        }
    }

    private static void MostrarInformacion(TablaAmortizacion tablaAmortizacion, string nombreCliente, double monto,
        int plazo, double porcentajeInteresAnual, string sistemaAmortizacion, double tipoCambioDolar)
    {
        // Mostrar información formateada
        Console.WriteLine("\nTipo de cambio compra BCCR: " + tipoCambioDolar.ToString("N2"));
        Console.WriteLine("\nDatos de la consulta:");
        Console.WriteLine("Cliente: " + nombreCliente);
        Console.WriteLine("Monto del préstamo otorgado: " + monto.ToString("N2") + " colones");
        Console.WriteLine("Plazo del préstamo: " + plazo + " años");
        Console.WriteLine("Interés anual: " + porcentajeInteresAnual.ToString("N2") + " %");
        Console.WriteLine("Sistema de amortización: " + sistemaAmortizacion);
        Console.WriteLine("\nTabla de Amortización");
        Console.WriteLine("Período\tDeuda inicial\tIntereses (sk)\tAmortización (vk)\tcuota (ck)");

        foreach (var fila in tablaAmortizacion.ToList())
        {
            Console.WriteLine(string.Join("\t", fila));
        }
        
        Console.WriteLine("Fecha: " + DateTime.Now);
    }
}