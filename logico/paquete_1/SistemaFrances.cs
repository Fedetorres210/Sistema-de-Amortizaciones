using System;
using System.Collections.Generic;
namespace logico.paquete_1
{
	public class SistemaFrances: SistemaPago
	{

        public SistemaFrances(double pCredito, int pTiempo, double pTasa_interes) : base(pCredito, pTiempo, pTasa_interes)
        {
            
        }

        public SistemaFrances(double pCredito, int pTiempo, double pTasa_interes, Convertidor convertidor) : base(pCredito, pTiempo, pTasa_interes,convertidor)
        {
           
        }

        public override Dictionary<string,List<double>> calcularAmenidadesCredito()
        {
            Dictionary<string, List<double>> amenidadesCredito = new Dictionary<string, List<double>>(){
                { "Cuota",new List<double>() },
                { "Tasa de interes", new List<double>()},
                { "Deuda Inicial", new List<double>() },
                { "Amortizacion", new List<double>() }
                
            };

            double cuota = (credito * tasa_interes) / (1 - Math.Pow(1 + tasa_interes, -tiempo));
            double totalInteres = 0;
            double deudaInicial = credito;
            

            for (int periodo = 1; periodo <= tiempo; periodo++)
            {
                    double interes = deudaInicial * tasa_interes;
                    double amortizacion = cuota - interes;
                    

                    amenidadesCredito["Cuota"].Add(cuota);
                    amenidadesCredito["Tasa de interes"].Add(interes);
                    amenidadesCredito["Deuda Inicial"].Add(deudaInicial);
                    amenidadesCredito["Amortizacion"].Add(amortizacion);

                    deudaInicial -= amortizacion;
                    totalInteres += interes;
             }

            amenidadesCredito["Cuota"].Add(amenidadesCredito["Cuota"].Sum());
            amenidadesCredito["Tasa de interes"].Add(amenidadesCredito["Tasa de interes"].Sum());
            amenidadesCredito["Deuda Inicial"].Add(0);
            amenidadesCredito["Amortizacion"].Add(amenidadesCredito["Amortizacion"].Sum());


            return amenidadesCredito;
            
        
                
        }


        
    }
}

