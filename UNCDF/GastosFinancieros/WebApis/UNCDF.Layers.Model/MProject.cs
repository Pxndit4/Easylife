using System;
namespace UNCDF.Layers.Model
{
    public class MProject
    {
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public int StartDate { get; set; }
        public string StartDateStr { get; set; }
        public int EndDate { get; set; }
        public string EndDateStr { get; set; }
        public string Title { get; set; }
        public string AwardId { get; set; }
        public string AwardStatus { get; set; }

        /***Agregados solo validacion**/
        public string EffectiveStatus { get; set; }
        public string StatusEffDateStr { get; set; }
        public int StatusEffDate { get; set; }
        public string Department { get; set; }

        public int StatusEffSeq { get; set; }
        public string StatusDescription { get; set; }

        public int Donation { get; set; }
        public int Advance { get; set; }
        /* Atributos para la carga de Imagen y Video */


        public string Image { get; set; }
        public string Video { get; set; }

        public string Ext { get; set; }
        public string ExtVideo { get; set; }
        public bool IsVisible { get; set; }
        public byte[] FileByte { get; set; }
        public byte[] VideoFileByte { get; set; }

        public string Flag { get; set; }
        //UTILIZADO PARA LOS FILTROS
        public string Continents { get; set; }
        public string Countries { get; set; }
        public string Anio { get; set; }
        public bool Donation { get; set; }
        
    }

    
}
