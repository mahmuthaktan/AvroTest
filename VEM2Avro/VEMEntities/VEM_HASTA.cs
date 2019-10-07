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
    public class VEM_HASTA
    {
        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public int HASTA_KODU { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string REFERANS_TABLO_ADI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string TC_KIMLIK_NUMARASI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string UYRUK { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string HASTA_TIPI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string AD { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public DateTime? DOGUM_TARIHI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string DOGUM_YERI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string CINSIYET { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public long? DOGUM_SIRASI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string ANNE_ADI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string BABA_ADI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string MEDENI_HALI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? MESLEK { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? OGRENIM_DURUMU { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string KAN_GRUBU { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? ENGELLILIK_DURUMU { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string OLUM_TARIHI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string OLUM_YERI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string PASAPORT_NUMARASI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string YUPASS_NUMARASI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string SON_KURUM_KODU { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public DateTime KAYIT_ZAMANI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? EKLEYEN_KULLANICI_KODU { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public DateTime? GUNCELLEME_ZAMANI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? GUNCELLEYEN_KULLANICI_KODU { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? ANNE_HASTA_KODU { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]

        public string ANNE_TC_KIMLIK_NUMARASI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? BABA_HASTA_KODU { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? BABA_TC_KIMLIK_NUMARASI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public DateTime? BEYAN_DOGUM_TARIHI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? KIMLIKSIZ_HASTA_BILGISI { get; set; }

        [DataMember]
        [NullableSchema, DefaultValue(null)]
        public int? MUAYENE_ONCELIK_SIRASI { get; set; }

        [DataMember]
        public string SOYADI { get; set; }

        public static IEnumerable<VEM_HASTA> GetInserted()
        {
            return DatabaseAccess.Query<VEM_HASTA>(@"select top 3 * from VEM_HASTA (nolock)",
                " ", null);
            //                new { fkCalisan = _fkCalisan });
        }




    }


}
