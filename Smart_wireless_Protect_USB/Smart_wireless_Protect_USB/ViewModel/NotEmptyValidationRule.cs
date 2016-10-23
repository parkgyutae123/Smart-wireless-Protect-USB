using System.Globalization;
using System.Windows.Controls;

namespace Smart_wireless_Protect_USB.ViewModel
{
    /// <summary>
    /// 입력란이 공백인지 아닌지에 대한 유효성검사
    /// </summary>
    public class NotEmptyValidationRule : ValidationRule
    {      
        /// <summary>
        /// 유효성 검사 메서드 오버라이딩
        /// </summary>
        /// <param name="value">입력값</param>
        /// <param name="cultureInfo">문화권</param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {         
            /// //??연산자는 ??왼쪽값이 null이면 ?? 오른쪽 피연산자를 반환 아니면 왼쪽 피연산자
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "Field is required.")
                : ValidationResult.ValidResult;
        }
    }
}
