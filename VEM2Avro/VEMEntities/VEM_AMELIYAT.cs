using Microsoft.Hadoop.Avro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using Vem2Avro.Data;

namespace AvroTestApp.VEMEntities
{
    [DataContract]
    public class VEM_AMELIYAT
    {
        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int AMELIYAT_KODU { get; set; }
        [DataMember]
        public string REFERANS_TABLO_ADI { get; set; }
        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? HASTA_BASVURU_KODU { get; set; }
        [DataMember]
        public string AMELIYAT_ADI { get; set; }
        [DataMember]
        public string AMELIYAT_TURU { get; set; }
        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public DateTime? AMELIYAT_BASLAMA_ZAMANI { get; set; }
        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public DateTime? AMELIYAT_BITIS_ZAMANI { get; set; }
        [DataMember]
        public string MASA_CIHAZ_KODU { get; set; }
        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? BIRIM_KODU { get; set; }
        [DataMember]
        public int DEFTER_NUMARASI { get; set; }
        [DataMember]
        public string AMELIYAT_DURUMU { get; set; }
        [DataMember]
        public string ANESTEZI_TURU { get; set; }
        [DataMember]
        public string AMELIYAT_TIPI { get; set; }
        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? SKOPI_SURESI { get; set; }
        [DataMember]
        public string PROFILAKSI_PERIYODU { get; set; }
        [DataMember]
        public string PROFILAKSI_KODU { get; set; }
        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? EKLEYEN_KULLANICI_KODU { get; set; }
        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? GUNCELLEYEN_KULLANICI_KODU { get; set; }
        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? ANESTEZI_BASLAMA_ZAMANI { get; set; }
        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? ANESTEZI_BITIS_ZAMANI { get; set; }
        [DataMember]
        public bool ANESTEZI_NOTU { get; set; }
        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public DateTime? GUNCELLEME_ZAMANI { get; set; }
        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? HASTA_KODU { get; set; }
        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public DateTime? KAYIT_ZAMANI { get; set; }

        public static IEnumerable<VEM_AMELIYAT> GetAmeliyat()
        {
            return DatabaseAccess.Query<VEM_AMELIYAT>(@"select * from VEM_AMELIYAT (nolock)",
                " ", null);
            //                new { fkCalisan = _fkCalisan });
        }




    }


}
