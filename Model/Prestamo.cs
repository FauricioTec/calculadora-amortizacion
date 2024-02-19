namespace Model;

public class Prestamo
{
    private string nombreCliente;
    private double monto;
    private int plazo;
    private double porcentajeInteresAnual;
    
    public Prestamo(string nombreCliente, double monto, int plazo, double porcentajeInteresAnual)
    {
        this.nombreCliente = nombreCliente;
        this.monto = monto;
        this.plazo = plazo;
        this.porcentajeInteresAnual = porcentajeInteresAnual;
    }
    
    public string NombreCliente
    {
        get { return nombreCliente; }
    }
    
    public double Monto
    {
        get { return monto; }
    }
    
    public int Plazo
    {
        get { return plazo; }
    }
    
    public double PorcentajeInteresAnual
    {
        get { return porcentajeInteresAnual; }
    }
}