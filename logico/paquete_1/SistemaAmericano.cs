using System;
namespace logico.paquete_1
{
	public class SistemaAmericano: SistemaPago
	{
        public SistemaAmericano(double pCredito, int pTiempo, double pTasa_interes) : base(pCredito, pTiempo, pTasa_interes)
        {

        }

        public SistemaAmericano(double pCredito, int pTiempo, double pTasa_interes, Convertidor convertidor) : base(pCredito, pTiempo, pTasa_interes,convertidor)
        {

        }

        public override Dictionary<string, List<double>> calcularAmenidadesCredito()
        {
            Dictionary<string, List<double>> amenidadesCredito = new Dictionary<string, List<double>>(){
                { "Cuota",new List<double>() },
                { "Tasa de interes", new List<double>()},
                { "Deuda Inicial", new List<double>() },
                { "Amortizacion", new List<double>() }
            };

            double cuota = credito * (1 + tasa_interes);
            double totalInteres = 0;
            double deudaInicial = credito;

            
            for (int i = 0; i < tiempo - 1; i++)
            {
                amenidadesCredito["Cuota"].Add(deudaInicial * tasa_interes);
                amenidadesCredito["Tasa de interes"].Add(deudaInicial * tasa_interes);
                amenidadesCredito["Deuda Inicial"].Add(deudaInicial);
                amenidadesCredito["Amortizacion"].Add(0);
            }

            
            amenidadesCredito["Cuota"].Add(cuota);
            amenidadesCredito["Tasa de interes"].Add(deudaInicial * tasa_interes);
            amenidadesCredito["Deuda Inicial"].Add(deudaInicial);
            amenidadesCredito["Amortizacion"].Add(deudaInicial);

            // Suma total de intereses
            totalInteres = tiempo * deudaInicial * tasa_interes;

            // Agregar la última fila con la suma total de cada columna
            amenidadesCredito["Cuota"].Add(amenidadesCredito["Cuota"].Sum());
            amenidadesCredito["Tasa de interes"].Add(totalInteres);
            amenidadesCredito["Deuda Inicial"].Add(0);
            amenidadesCredito["Amortizacion"].Add(amenidadesCredito["Amortizacion"].Sum());

            return amenidadesCredito;
        }

            


        }




    }



