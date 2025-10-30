using Dominio.Aplicacion.Comandos;
using Dominio.Aplicacion.Comunes;
using Dominio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Aplicacion.CasosDeUso
{
    public class AplicarDescuentoVipHandler
    {
        private readonly ServicioPromocion _servicioPromocion;

        public AplicarDescuentoVipHandler(ServicioPromocion servicioPromocion)
        {
            _servicioPromocion = servicioPromocion;
        }

        public async Task<Result> Handle(AplicarDescuentoVipCommand command)//Obtiene la informacion
        {
            try
            {
                await _servicioPromocion.AplicarDescuentoVip(//Llama a Servicio para verificar
                    command.ClienteId,
                    command.Categoria,
                    command.Porcentaje
                );

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error al aplicar descuento: {ex.Message}");
            }
        }
    }

}