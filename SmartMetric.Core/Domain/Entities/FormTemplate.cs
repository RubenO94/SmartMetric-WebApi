using SmartMetric.Core.DTO;
using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SmartMetric.Core.Domain.Entities
{
    public class FormTemplate
    {
        [Key]
        public Guid FormTemplateId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public virtual ICollection<FormTemplateTranslation>? Translations { get; set; }
        public virtual ICollection<FormTemplateQuestion>? FormTemplateQuestions { get; set; }
    }


    //public static FormTemplateDTOResponse ToFormTemplateResponse(this FormTemplate formTemplate, Language? language)
    //{
    //    Language languageSelected = Language.PT;
    //    string? title = string.Empty;
    //    string? description = string.Empty;

    //    if(language != null)
    //    {
    //        languageSelected = (Language)language;
    //    }

    //    FormTemplateTranslation? translation = formTemplate.Translations.FirstOrDefault(temp => temp.Language == languageSelected.ToString());
    //    if(translation != null)
    //    {
    //        title = translation.Title;
    //        description = translation.Description;
    //    }

    //    return new FormTemplateDTOResponse()
    //    {

    //    };


    //}

}
