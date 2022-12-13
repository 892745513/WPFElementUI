using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace dps
{
    public class TextBoxEx
    {
        static TextBoxEx()
        {
            SubHandler += SubHandlerClick;
            AddHandler += AddHandlerClick;


            YeadsDefaults = new List<int>();
            MonthsDefaults = new List<int>();

            for (int i = 1900; i <= 2300; i++)
            {
                YeadsDefaults.Add(i);
            }
            for (int i = 1; i <= 12; i++)
            {
                MonthsDefaults.Add(i);
            }

            DateOkHandler += DateOkHandlerClick;
            DateCancleHandler += DateCancleClick;

            GotFocusDateBox += GotFocusDateBoxClick;
        }

        #region 是否有清除按钮
        // Register an attached dependency property with the specified
        // property name, property type, owner type, and property metadata.
        public static readonly DependencyProperty HasClearProperty =
            DependencyProperty.RegisterAttached(
          "HasClear",
          typeof(bool),
          typeof(TextBoxEx),
          new FrameworkPropertyMetadata(defaultValue: false,
              flags: FrameworkPropertyMetadataOptions.AffectsRender)
        );

        // Declare a get accessor method.
        public static bool GetHasClear(TextBox target) =>
            (bool)target.GetValue(HasClearProperty);

        // Declare a set accessor method.
        public static void SetHasClear(TextBox target, bool value) =>
            target.SetValue(HasClearProperty, value);
        #endregion


        #region 是否有提示符
        // Register an attached dependency property with the specified
        // property name, property type, owner type, and property metadata.
        public static readonly DependencyProperty PlaceHolderProperty =
            DependencyProperty.RegisterAttached(
          "PlaceHolder",
          typeof(string),
          typeof(TextBoxEx),
          new FrameworkPropertyMetadata(defaultValue: "",
              flags: FrameworkPropertyMetadataOptions.AffectsRender)
        );

        // Declare a get accessor method.
        public static string GetPlaceHolder(TextBox target) => (string)target.GetValue(PlaceHolderProperty);

        // Declare a set accessor method.
        public static void SetPlaceHolder(TextBox target, string value) => target.SetValue(PlaceHolderProperty, value);
        #endregion


        #region 数字框按钮

        #region 是否数字框
        /// <summary>
        /// 是否数字框
        /// </summary>
        public static readonly DependencyProperty IsNumberBoxProperty =
            DependencyProperty.RegisterAttached(
          "IsNumberBox",
          typeof(bool),
          typeof(TextBoxEx),
          new FrameworkPropertyMetadata(defaultValue: false,
              flags: FrameworkPropertyMetadataOptions.AffectsRender)
        );

        // Declare a get accessor method.
        public static bool GetIsNumberBox(TextBox target) => (bool)target.GetValue(IsNumberBoxProperty);

        // Declare a set accessor method.
        public static void SetIsNumberBox(TextBox target, bool value) => target.SetValue(IsNumberBoxProperty, value);
        #endregion

        #region 最大值
        /// <summary>
        /// 最大值
        /// </summary>
        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.RegisterAttached(
          "Max",
          typeof(double),
          typeof(TextBoxEx),
          new FrameworkPropertyMetadata(defaultValue: double.MaxValue,
              flags: FrameworkPropertyMetadataOptions.AffectsRender)
        );

        // Declare a get accessor method.
        public static double GetMax(TextBox target) => (double)target.GetValue(MaxProperty);

        // Declare a set accessor method.
        public static void SetMax(TextBox target, double value) => target.SetValue(MaxProperty, value);

        #endregion

        #region 最小值
        /// <summary>
        /// 最小值
        /// </summary>
        public static readonly DependencyProperty MinProperty =
            DependencyProperty.RegisterAttached(
          "Min",
          typeof(double),
          typeof(TextBoxEx),
          new FrameworkPropertyMetadata(defaultValue: double.MinValue,
              flags: FrameworkPropertyMetadataOptions.AffectsRender)
        );

        // Declare a get accessor method.
        public static double GetMin(TextBox target) => (double)target.GetValue(MinProperty);

        // Declare a set accessor method.
        public static void SetMin(TextBox target, double value) => target.SetValue(MinProperty, value);

        #endregion

        #region 步长
        /// <summary>
        /// 步长
        /// </summary>
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.RegisterAttached(
          "Step",
          typeof(double),
          typeof(TextBoxEx),
          new FrameworkPropertyMetadata(defaultValue: 1.0,
              flags: FrameworkPropertyMetadataOptions.AffectsRender)
        );

        // Declare a get accessor method.
        public static double GetStep(TextBox target) => (double)target.GetValue(StepProperty);

        // Declare a set accessor method.
        public static void SetStep(TextBox target, double value) => target.SetValue(StepProperty, value);
        #endregion

        /// <summary>
        /// 减少按钮
        /// </summary>
        public static RoutedEventHandler SubHandler;

        /// <summary>
        /// 增加按钮
        /// </summary>
        public static RoutedEventHandler AddHandler;
        public static void SubHandlerClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = sender as FrameworkElement;
                var tbx = VisualTreeHelperEx.FindVisualParent<TextBox>(btn);
                if (tbx == null)
                {
                    return;
                }
                if (!double.TryParse(tbx.Text, out var value))
                {
                    tbx.Text = "0";
                    return;
                }

                var newvalue = (value - GetStep(tbx));
                var min = GetMin(tbx);
                tbx.Text = newvalue <= min ? min.ToString() : newvalue.ToString();
            }
            catch (Exception)
            {

            }

        }
        public static void AddHandlerClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = sender as FrameworkElement;

                var tbx = VisualTreeHelperEx.FindVisualParent<TextBox>(btn);
                if (tbx == null)
                {
                    return;
                }
                if (!double.TryParse(tbx.Text, out var value))
                {
                    tbx.Text = "0";
                    return;
                }

                var newvalue = (value + GetStep(tbx));
                var max = GetMax(tbx);
                tbx.Text = newvalue >= max ? max.ToString() : newvalue.ToString();
            }
            catch (Exception)
            {
            }

        }


        #endregion

        #region 
        #region 是否日期选择框
        /// <summary>
        /// 是否日期选择框
        /// </summary>
        public static readonly DependencyProperty IsDateProperty =
            DependencyProperty.RegisterAttached(
          "IsDate",
          typeof(bool),
          typeof(TextBoxEx),
          new FrameworkPropertyMetadata(defaultValue: false,
              flags: FrameworkPropertyMetadataOptions.AffectsRender)
        );

        // Declare a get accessor method.
        public static bool GetIsDate(TextBox target) => (bool)target.GetValue(IsDateProperty);

        // Declare a set accessor method.
        public static void SetIsDate(TextBox target, bool value) => target.SetValue(IsDateProperty, value);
        #endregion

        #region 年选择框范围

        public static List<int> YeadsDefaults { get; set; }
        /// <summary>
        ///  日期选择框范围
        /// </summary>
        public static readonly DependencyProperty YearsProperty =
            DependencyProperty.RegisterAttached(
          "Years",
          typeof(List<int>),
          typeof(TextBoxEx),
          new FrameworkPropertyMetadata(defaultValue: YeadsDefaults,
              flags: FrameworkPropertyMetadataOptions.AffectsRender)
        );

        // Declare a get accessor method.
        public static List<int> GetYears(TextBox target) => (List<int>)target.GetValue(YearsProperty);

        // Declare a set accessor method.
        public static void SetYears(TextBox target, List<int> value) => target.SetValue(YearsProperty, value);
        #endregion

        #region 月选择框范围

        public static List<int> MonthsDefaults { get; set; }
        /// <summary>
        ///  日期选择框范围
        /// </summary>
        public static readonly DependencyProperty MonthsProperty =
            DependencyProperty.RegisterAttached(
          "Months",
          typeof(List<int>),
          typeof(TextBoxEx),
          new FrameworkPropertyMetadata(defaultValue: MonthsDefaults,
              flags: FrameworkPropertyMetadataOptions.AffectsRender)
        );

        // Declare a get accessor method.
        public static List<int> GetMonths(TextBox target) => (List<int>)target.GetValue(MonthsProperty);

        // Declare a set accessor method.
        public static void SetMonths(TextBox target, List<int> value) => target.SetValue(MonthsProperty, value);
        #endregion

        #region 是否显示选择框

        /// <summary>
        ///  日期选择框范围
        /// </summary>
        public static readonly DependencyProperty ShowPopProperty =
            DependencyProperty.RegisterAttached(
          "ShowPop",
          typeof(bool),
          typeof(TextBoxEx),
          new FrameworkPropertyMetadata(defaultValue: false,
              flags: FrameworkPropertyMetadataOptions.AffectsRender)
        );

        // Declare a get accessor method.
        public static bool GetShowPop(TextBox target) => (bool)target.GetValue(ShowPopProperty);

        // Declare a set accessor method.
        public static void SetShowPop(TextBox target, bool value) => target.SetValue(ShowPopProperty, value);
        #endregion

        #region 选中年

        /// <summary>
        ///  选中年
        /// </summary>
        public static readonly DependencyProperty SelectedYearProperty =
            DependencyProperty.RegisterAttached(
          "SelectedYear",
          typeof(int),
          typeof(TextBoxEx),
          new FrameworkPropertyMetadata(defaultValue: DateTime.Now.Year,
              flags: FrameworkPropertyMetadataOptions.AffectsRender)
        );

        // Declare a get accessor method.
        public static int GetSelectedYear(TextBox target) => (int)target.GetValue(SelectedYearProperty);

        // Declare a set accessor method.
        public static void SetSelectedYear(TextBox target, int value) => target.SetValue(SelectedYearProperty, value);
        #endregion

        #region 选中月

        /// <summary>
        ///  选中年
        /// </summary>
        public static readonly DependencyProperty SelectedMonthProperty =
            DependencyProperty.RegisterAttached(
          "SelectedMonth",
          typeof(int),
          typeof(TextBoxEx),
          new FrameworkPropertyMetadata(defaultValue: DateTime.Now.Month,
              flags: FrameworkPropertyMetadataOptions.AffectsRender)
        );

        // Declare a get accessor method.
        public static int GetSelectedMonth(TextBox target) => (int)target.GetValue(SelectedMonthProperty);

        // Declare a set accessor method.
        public static void SetSelectedMonth(TextBox target, int value) => target.SetValue(SelectedMonthProperty, value);
        #endregion

        /// <summary>
        /// 日期框确认
        /// </summary>
        public static RoutedEventHandler DateOkHandler;

        /// <summary>
        /// 日期框取消
        /// </summary>
        public static RoutedEventHandler DateCancleHandler;
        public static RoutedEventHandler GotFocusDateBox;

        private static void GotFocusDateBoxClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var bd = sender as FrameworkElement;
                if (bd != null)
                {
                    var tbx = VisualTreeHelperEx.FindVisualParent<TextBox>(bd);
                    if (tbx != null)
                    {
                        SetShowPop(tbx, true);
                    }
                } 
            }
            catch (Exception)
            {

            }
        }
        public static void DateOkHandlerClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = sender as FrameworkElement;
                var scro = btn.DataContext as FrameworkElement;
                var tbx = VisualTreeHelperEx.FindVisualParent<TextBox>(scro);
                if (tbx != null)
                {
                    SetShowPop(tbx, false);
                    var selectDate = GetSelectedYear(tbx);
                    var selectMonth = GetSelectedMonth(tbx);

                    tbx.Text = $"{selectDate}-{selectMonth}";
                }
            }
            catch (Exception)
            {

            }

        }
        public static void DateCancleClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = sender as FrameworkElement;
                var scro = btn.DataContext as FrameworkElement;
                var tbx = VisualTreeHelperEx.FindVisualParent<TextBox>(scro);
                if (tbx != null)
                {
                    SetShowPop(tbx, false);

                }
            }
            catch (Exception)
            {

            }

        }
        #endregion
    }
}
