namespace TestBodega.Util
{
    public class Enum
    {
        public enum TipoArchivos
        {
            Licencia,
            RUNT,
            SIMIT,
            CursoManejoDefensivo,
            CursoManejoDefensivoTerrenoAgreste,
            CursoMecanicaBasica,
            CursoPrimerosAuxilios,
            CursoManejoExtintores,
            CursoAlistamientoVehiculos,
            SeguridadSocial,
            ExamenesMedicos,
            CertificadoResidencia,
            ARL,
            EPS,
            AFP,
            CCF,
            CarnetEcopetrolFrontera,
            FotoVevhiculo = 1002,
            SOAT = 1003,
            RevisionTecnoMecanica = 1004,
            TarjetaDeOperacion = 1005,
            PolizaResponsabilidadCivilContractual = 1006,
            PolizaResponsabilidadCivilExtracontractual = 1007,
            PolizaTodoRiesgo = 1008,
            InspeccionBimestral = 1009,
            CertificacionGPS = 1010,
            Cotrato = 1011
        }

        public enum EnumEstadoConductor
        {
            Activo = 1,
            Inactivo = 2,
            Incompleto = 3,
            Eliminado = 4
        }
        
        public enum EnumModulo
        {
            Conductores = 1,
            Vehiculos = 2,
            Contratos = 3
        }
    }
}
