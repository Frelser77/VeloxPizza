namespace VeloxPizza.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ordine")]
    public partial class Ordine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ordine()
        {
            ProdottoAcquistato = new HashSet<ProdottoAcquistato>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdOrdine { get; set; }

        public int IdUtente { get; set; }

        [Required]
        [StringLength(255)]
        public string IndirizzoDiConsegna { get; set; }

        public DateTime DataOrdine { get; set; }

        public bool IsEvaso { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Nota { get; set; }

        public double? PrezzoTotale { get; set; }

        public virtual Utente Utente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProdottoAcquistato> ProdottoAcquistato { get; set; }
    }
}
