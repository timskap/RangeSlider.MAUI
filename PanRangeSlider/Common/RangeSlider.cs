using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls.Shapes;
using static System.Math;
using static Microsoft.Maui.Controls.AbsoluteLayout;

namespace PanRangeSlider;

public class RangeSlider : TemplatedView
{
    const double enabledOpacity = 1;

    const double disabledOpacity = .6;

    const int labelElementFontSize = 12;

    public event EventHandler? ValueChanged;

    public event EventHandler? LowerValueChanged;

    public event EventHandler? UpperValueChanged;

    public event EventHandler? DragStarted;

    public event EventHandler? LowerDragStarted;

    public event EventHandler? UpperDragStarted;

    public event EventHandler? DragCompleted;

    public event EventHandler? LowerDragCompleted;

    public event EventHandler? UpperDragCompleted;

    public static BindableProperty ThumbCornerRadiusProperty =
    BindableProperty.Create(nameof(ThumbCornerRadius), typeof(double), typeof(RangeSlider), 10.0, propertyChanged: OnLayoutPropertyChanged);

public static BindableProperty LowerThumbCornerRadiusProperty =
    BindableProperty.Create(nameof(LowerThumbCornerRadius), typeof(double), typeof(RangeSlider), -1.0, propertyChanged: OnLayoutPropertyChanged);

public static BindableProperty UpperThumbCornerRadiusProperty =
    BindableProperty.Create(nameof(UpperThumbCornerRadius), typeof(double), typeof(RangeSlider), -1.0, propertyChanged: OnLayoutPropertyChanged);

public double ThumbCornerRadius
{
    get => (double)GetValue(ThumbCornerRadiusProperty);
    set => SetValue(ThumbCornerRadiusProperty, value);
}

public double LowerThumbCornerRadius
{
    get => (double)GetValue(LowerThumbCornerRadiusProperty);
    set => SetValue(LowerThumbCornerRadiusProperty, value);
}

public double UpperThumbCornerRadius
{
    get => (double)GetValue(UpperThumbCornerRadiusProperty);
    set => SetValue(UpperThumbCornerRadiusProperty, value);
}

// Track Corner Radius
public static BindableProperty TrackCornerRadiusProperty =
    BindableProperty.Create(nameof(TrackCornerRadius), typeof(double), typeof(RangeSlider), 10.0, propertyChanged: OnLayoutPropertyChanged);

public static BindableProperty TrackHighlightCornerRadiusProperty =
    BindableProperty.Create(nameof(TrackHighlightCornerRadius), typeof(double), typeof(RangeSlider), 10.0, propertyChanged: OnLayoutPropertyChanged);

// Track Background Color
public static BindableProperty TrackBackgroundColorProperty =
    BindableProperty.Create(nameof(TrackBackgroundColor), typeof(Color), typeof(RangeSlider), Colors.Gray, propertyChanged: OnLayoutPropertyChanged);

public static BindableProperty TrackHighlightBackgroundColorProperty =
    BindableProperty.Create(nameof(TrackHighlightBackgroundColor), typeof(Color), typeof(RangeSlider), Colors.Blue, propertyChanged: OnLayoutPropertyChanged);

public double TrackCornerRadius
{
    get => (double)GetValue(TrackCornerRadiusProperty);
    set => SetValue(TrackCornerRadiusProperty, value);
}

public double TrackHighlightCornerRadius
{
    get => (double)GetValue(TrackHighlightCornerRadiusProperty);
    set => SetValue(TrackHighlightCornerRadiusProperty, value);
}

public Color TrackBackgroundColor
{
    get => (Color)GetValue(TrackBackgroundColorProperty);
    set => SetValue(TrackBackgroundColorProperty, value);
}

public Color TrackHighlightBackgroundColor
{
    get => (Color)GetValue(TrackHighlightBackgroundColorProperty);
    set => SetValue(TrackHighlightBackgroundColorProperty, value);
}

    public static BindableProperty MinimumValueProperty
        = BindableProperty.Create(nameof(MinimumValue), typeof(double), typeof(RangeSlider), .0, propertyChanged: OnMinimumMaximumValuePropertyChanged);

    public static BindableProperty MaximumValueProperty
        = BindableProperty.Create(nameof(MaximumValue), typeof(double), typeof(RangeSlider), 1.0, propertyChanged: OnMinimumMaximumValuePropertyChanged);

    public static BindableProperty StepValueProperty
        = BindableProperty.Create(nameof(StepValue), typeof(double), typeof(RangeSlider), 0.0, propertyChanged: OnMinimumMaximumValuePropertyChanged);

    public static BindableProperty LowerValueProperty
        = BindableProperty.Create(nameof(LowerValue), typeof(double), typeof(RangeSlider), .0, BindingMode.TwoWay, propertyChanged: OnLowerUpperValuePropertyChanged, coerceValue: CoerceValue);

    public static BindableProperty UpperValueProperty
        = BindableProperty.Create(nameof(UpperValue), typeof(double), typeof(RangeSlider), 1.0, BindingMode.TwoWay, propertyChanged: OnLowerUpperValuePropertyChanged, coerceValue: CoerceValue);

    public static BindableProperty ThumbSizeProperty
        = BindableProperty.Create(nameof(ThumbSize), typeof(double), typeof(RangeSlider), 28.0, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty LowerThumbSizeProperty
        = BindableProperty.Create(nameof(LowerThumbSize), typeof(double), typeof(RangeSlider), -1.0, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty UpperThumbSizeProperty
        = BindableProperty.Create(nameof(UpperThumbSize), typeof(double), typeof(RangeSlider), -1.0, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty TrackSizeProperty
        = BindableProperty.Create(nameof(TrackSize), typeof(double), typeof(RangeSlider), 4.0, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty ThumbColorProperty
        = BindableProperty.Create(nameof(ThumbColor), typeof(Color), typeof(RangeSlider), null, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty LowerThumbColorProperty
        = BindableProperty.Create(nameof(LowerThumbColor), typeof(Color), typeof(RangeSlider), null, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty UpperThumbColorProperty
        = BindableProperty.Create(nameof(UpperThumbColor), typeof(Color), typeof(RangeSlider), null, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty TrackColorProperty
        = BindableProperty.Create(nameof(TrackColor), typeof(Color), typeof(RangeSlider), null, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty TrackHighlightColorProperty
        = BindableProperty.Create(nameof(TrackHighlightColor), typeof(Color), typeof(RangeSlider), null, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty ThumbBorderColorProperty
        = BindableProperty.Create(nameof(ThumbBorderColor), typeof(Color), typeof(RangeSlider), null, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty LowerThumbBorderColorProperty
        = BindableProperty.Create(nameof(LowerThumbBorderColor), typeof(Color), typeof(RangeSlider),null, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty UpperThumbBorderColorProperty
        = BindableProperty.Create(nameof(UpperThumbBorderColor), typeof(Color), typeof(RangeSlider), null, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty TrackBorderColorProperty
        = BindableProperty.Create(nameof(TrackBorderColor), typeof(Color), typeof(RangeSlider), null, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty TrackHighlightBorderColorProperty
        = BindableProperty.Create(nameof(TrackHighlightBorderColor), typeof(Color), typeof(RangeSlider), null, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty ValueLabelStyleProperty
        = BindableProperty.Create(nameof(ValueLabelStyle), typeof(Style), typeof(RangeSlider), propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty LowerValueLabelStyleProperty
        = BindableProperty.Create(nameof(LowerValueLabelStyle), typeof(Style), typeof(RangeSlider), propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty UpperValueLabelStyleProperty
        = BindableProperty.Create(nameof(UpperValueLabelStyle), typeof(Style), typeof(RangeSlider), propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty ValueLabelStringFormatProperty
        = BindableProperty.Create(nameof(ValueLabelStringFormat), typeof(string), typeof(RangeSlider), "{0:0.##}", propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty LowerThumbViewProperty
        = BindableProperty.Create(nameof(LowerThumbView), typeof(View), typeof(RangeSlider), propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty UpperThumbViewProperty
        = BindableProperty.Create(nameof(UpperThumbView), typeof(View), typeof(RangeSlider), propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty ValueLabelSpacingProperty
        = BindableProperty.Create(nameof(ValueLabelSpacing), typeof(double), typeof(RangeSlider), 5.0, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty ThumbRadiusProperty
        = BindableProperty.Create(nameof(ThumbRadius), typeof(double), typeof(RangeSlider), -1.0, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty LowerThumbRadiusProperty
        = BindableProperty.Create(nameof(LowerThumbRadius), typeof(double), typeof(RangeSlider), -1.0, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty UpperThumbRadiusProperty
        = BindableProperty.Create(nameof(UpperThumbRadius), typeof(double), typeof(RangeSlider), -1.0, propertyChanged: OnLayoutPropertyChanged);

    public static BindableProperty TrackRadiusProperty
        = BindableProperty.Create(nameof(TrackRadius), typeof(double), typeof(RangeSlider), -1.0, propertyChanged: OnLayoutPropertyChanged);

    readonly Dictionary<View, double> thumbPositionMap = new Dictionary<View, double>();

    readonly PanGestureRecognizer lowerThumbGestureRecognizer = new PanGestureRecognizer();

    readonly PanGestureRecognizer upperThumbGestureRecognizer = new PanGestureRecognizer();

    Size allocatedSize;

    double labelMaxHeight;

    double lowerTranslation;

    double upperTranslation;

    int dragCount;

    public RangeSlider()
        => ControlTemplate = new ControlTemplate(typeof(AbsoluteLayout));

    public double MinimumValue
    {
        get => (double)GetValue(MinimumValueProperty);
        set => SetValue(MinimumValueProperty, value);
    }

    public double MaximumValue
    {
        get => (double)GetValue(MaximumValueProperty);
        set => SetValue(MaximumValueProperty, value);
    }

    public double StepValue
    {
        get => (double)GetValue(StepValueProperty);
        set => SetValue(StepValueProperty, value);
    }

    public double LowerValue
    {
        get => (double)GetValue(LowerValueProperty);
        set => SetValue(LowerValueProperty, value);
    }

    public double UpperValue
    {
        get => (double)GetValue(UpperValueProperty);
        set => SetValue(UpperValueProperty, value);
    }

    public double ThumbSize
    {
        get => (double)GetValue(ThumbSizeProperty);
        set => SetValue(ThumbSizeProperty, value);
    }

    public double LowerThumbSize
    {
        get => (double)GetValue(LowerThumbSizeProperty);
        set => SetValue(LowerThumbSizeProperty, value);
    }

    public double UpperThumbSize
    {
        get => (double)GetValue(UpperThumbSizeProperty);
        set => SetValue(UpperThumbSizeProperty, value);
    }

    public double TrackSize
    {
        get => (double)GetValue(TrackSizeProperty);
        set => SetValue(TrackSizeProperty, value);
    }

    public Color? ThumbColor
    {
        get => (Color?)GetValue(ThumbColorProperty);
        set => SetValue(ThumbColorProperty, value);
    }

    public Color? LowerThumbColor
    {
        get => (Color?)GetValue(LowerThumbColorProperty);
        set => SetValue(LowerThumbColorProperty, value);
    }

    public Color? UpperThumbColor
    {
        get => (Color?)GetValue(UpperThumbColorProperty);
        set => SetValue(UpperThumbColorProperty, value);
    }

    public Color? TrackColor
    {
        get => (Color?)GetValue(TrackColorProperty);
        set => SetValue(TrackColorProperty, value);
    }

    public Color? TrackHighlightColor
    {
        get => (Color?)GetValue(TrackHighlightColorProperty);
        set => SetValue(TrackHighlightColorProperty, value);
    }

    public Color? ThumbBorderColor
    {
        get => (Color?)GetValue(ThumbBorderColorProperty);
        set => SetValue(ThumbBorderColorProperty, value);
    }

    public Color? LowerThumbBorderColor
    {
        get => (Color?)GetValue(LowerThumbBorderColorProperty);
        set => SetValue(LowerThumbBorderColorProperty, value);
    }

    public Color? UpperThumbBorderColor
    {
        get => (Color?)GetValue(UpperThumbBorderColorProperty);
        set => SetValue(UpperThumbBorderColorProperty, value);
    }

    public Color? TrackBorderColor
    {
        get => (Color?)GetValue(TrackBorderColorProperty);
        set => SetValue(TrackBorderColorProperty, value);
    }

    public Color? TrackHighlightBorderColor
    {
        get => (Color?)GetValue(TrackHighlightBorderColorProperty);
        set => SetValue(TrackHighlightBorderColorProperty, value);
    }

    public Style ValueLabelStyle
    {
        get => (Style)GetValue(ValueLabelStyleProperty);
        set => SetValue(ValueLabelStyleProperty, value);
    }

    public Style LowerValueLabelStyle
    {
        get => (Style)GetValue(LowerValueLabelStyleProperty);
        set => SetValue(LowerValueLabelStyleProperty, value);
    }

    public Style UpperValueLabelStyle
    {
        get => (Style)GetValue(UpperValueLabelStyleProperty);
        set => SetValue(UpperValueLabelStyleProperty, value);
    }

    public string ValueLabelStringFormat
    {
        get => (string)GetValue(ValueLabelStringFormatProperty);
        set => SetValue(ValueLabelStringFormatProperty, value);
    }

    public View? LowerThumbView
    {
        get => (View?)GetValue(LowerThumbViewProperty);
        set => SetValue(LowerThumbViewProperty, value);
    }

    public View? UpperThumbView
    {
        get => (View?)GetValue(UpperThumbViewProperty);
        set => SetValue(UpperThumbViewProperty, value);
    }

    public double ValueLabelSpacing
    {
        get => (double)GetValue(ValueLabelSpacingProperty);
        set => SetValue(ValueLabelSpacingProperty, value);
    }

    public double ThumbRadius
    {
        get => (double)GetValue(ThumbRadiusProperty);
        set => SetValue(ThumbRadiusProperty, value);
    }

    public double LowerThumbRadius
    {
        get => (double)GetValue(LowerThumbRadiusProperty);
        set => SetValue(LowerThumbRadiusProperty, value);
    }

    public double UpperThumbRadius
    {
        get => (double)GetValue(UpperThumbRadiusProperty);
        set => SetValue(UpperThumbRadiusProperty, value);
    }

    public double TrackRadius
    {
        get => (double)GetValue(TrackRadiusProperty);
        set => SetValue(TrackRadiusProperty, value);
    }

    static bool IsThumbShadowSupported => false;
    // TODO: Uncomment when shadow is supported by these platforms
    // => DeviceInfo.Platform == DevicePlatform.iOS
    // || DeviceInfo.Platform == DevicePlatform.macOS 
    // || DeviceInfo.Platform == DevicePlatform.MacCatalyst;


    AbsoluteLayout? Control { get; set; }

    Border Track { get; } = CreateBorderElement<Border>();

    Border TrackHighlight { get; } = CreateBorderElement<Border>();

    Border LowerThumb { get; } = CreateBorderElement<ThumbBorder>(IsThumbShadowSupported);

    Border UpperThumb { get; } = CreateBorderElement<ThumbBorder>(IsThumbShadowSupported);

    Label LowerValueLabel { get; } = CreateLabelElement();

    Label UpperValueLabel { get; } = CreateLabelElement();

    double TrackWidth => Width - LowerThumb.Width - UpperThumb.Width;

    internal static void Preserve()
    {
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        base.OnPropertyChanged(propertyName);
        switch (propertyName)
        {
            case nameof(IsEnabled):
                OnIsEnabledChanged();
                break;
            case nameof(LowerValue):
                RaiseEvent(LowerValueChanged);
                RaiseEvent(ValueChanged);
                break;
            case nameof(UpperValue):
                RaiseEvent(UpperValueChanged);
                RaiseEvent(ValueChanged);
                break;
        }
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (width == allocatedSize.Width && height == allocatedSize.Height)
            return;

        allocatedSize = new Size(width, height);
        OnLayoutPropertyChanged();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (Control != null)
            Control.BindingContext = BindingContext;
    }

    protected override void OnChildAdded(Element child)
    {
        if (Control == null && child is AbsoluteLayout control)
        {
            Control = control;
            control.Children.Add(Track);
            control.Children.Add(TrackHighlight);
            control.Children.Add(LowerThumb);
            control.Children.Add(UpperThumb);
            control.Children.Add(LowerValueLabel);
            control.Children.Add(UpperValueLabel);

            AddGestureRecognizer(LowerThumb, lowerThumbGestureRecognizer);
            AddGestureRecognizer(UpperThumb, upperThumbGestureRecognizer);
            Track.SizeChanged += OnViewSizeChanged;
            LowerThumb.SizeChanged += OnViewSizeChanged;
            UpperThumb.SizeChanged += OnViewSizeChanged;
            LowerValueLabel.SizeChanged += OnViewSizeChanged;
            UpperValueLabel.SizeChanged += OnViewSizeChanged;
            OnIsEnabledChanged();
            OnLayoutPropertyChanged();
        }

        base.OnChildAdded(child);
    }

static Border CreateBorderElement<TBorder>(bool hasShadow = false) where TBorder : Border, new()
{
    TBorder border = new()
    {
        Padding = 0,
        StrokeShape = new RoundRectangle
        {
            CornerRadius = new CornerRadius(10) // Adjust for rounded corners
        }
    };
    return border;
}

    static IShape CreateRoundRectangleShape(CornerRadius cornerRadius)
        => new RoundRectangle()
        {
            CornerRadius = cornerRadius
        };


    static Label CreateLabelElement()
        => new Label
        {
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            LineBreakMode = LineBreakMode.NoWrap,
            FontSize = labelElementFontSize,
        };

    static object CoerceValue(BindableObject bindable, object value)
        => ((RangeSlider)bindable).CoerceValue((double)value);

    static void OnMinimumMaximumValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((RangeSlider)bindable).OnMinimumMaximumValuePropertyChanged();
        OnLowerUpperValuePropertyChanged(bindable, oldValue, newValue);
    }

    static void OnLowerUpperValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        => ((RangeSlider)bindable).OnLowerUpperValuePropertyChanged();

    static void OnLayoutPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        => ((RangeSlider)bindable).OnLayoutPropertyChanged();

    void OnIsEnabledChanged()
    {
        if (Control == null)
            return;

        Control.Opacity = IsEnabled
            ? enabledOpacity
            : disabledOpacity;
    }

    double CoerceValue(double value)
    {
        if (StepValue > 0 && value < MaximumValue)
        {
            var stepIndex = (int)((value - MinimumValue) / StepValue);
            value = MinimumValue + (stepIndex * StepValue);
        }
        return Clamp(value, MinimumValue, MaximumValue);
    }

    void OnMinimumMaximumValuePropertyChanged()
    {
        LowerValue = CoerceValue(LowerValue);
        UpperValue = CoerceValue(UpperValue);
    }

    void OnLowerUpperValuePropertyChanged()
    {
        var rangeValue = MaximumValue - MinimumValue;
        var trackWidth = TrackWidth;

        lowerTranslation = (LowerValue - MinimumValue) / rangeValue * trackWidth;
        upperTranslation = ((UpperValue - MinimumValue) / rangeValue * trackWidth) + LowerThumb.Width;

        LowerThumb.TranslationX = lowerTranslation;
        UpperThumb.TranslationX = upperTranslation;
        OnValueLabelTranslationChanged();

        var bounds = GetLayoutBounds(TrackHighlight);
        SetLayoutBounds(TrackHighlight, new Rect(lowerTranslation, bounds.Y, upperTranslation - lowerTranslation + UpperThumb.Width, bounds.Height));
    }

    void OnValueLabelTranslationChanged()
    {
        var labelSpacing = 5;
        var lowerLabelTranslation = lowerTranslation + ((LowerThumb.Width - LowerValueLabel.Width) / 2);
        var upperLabelTranslation = upperTranslation + ((UpperThumb.Width - UpperValueLabel.Width) / 2);
        LowerValueLabel.TranslationX = Min(Max(lowerLabelTranslation, 0), Width - LowerValueLabel.Width - UpperValueLabel.Width - labelSpacing);
        UpperValueLabel.TranslationX = Min(Max(upperLabelTranslation, LowerValueLabel.TranslationX + LowerValueLabel.Width + labelSpacing), Width - UpperValueLabel.Width);
    }

    void OnLayoutPropertyChanged()
    {
        BatchBegin();
        Track.BatchBegin();
        TrackHighlight.BatchBegin();
        LowerThumb.BatchBegin();
        UpperThumb.BatchBegin();
        LowerValueLabel.BatchBegin();
        UpperValueLabel.BatchBegin();

        var lowerThumbColor = GetColorOrDefault(LowerThumbColor, ThumbColor);
        var upperThumbColor = GetColorOrDefault(UpperThumbColor, ThumbColor);
        var lowerThumbBorderColor = GetColorOrDefault(LowerThumbBorderColor, ThumbBorderColor);
        var upperThumbBorderColor = GetColorOrDefault(UpperThumbBorderColor, ThumbBorderColor);
        if (!IsThumbShadowSupported)
        {
            var defaultThumbColor = Color.FromRgb(182, 182, 182);
            lowerThumbBorderColor = GetColorOrDefault(lowerThumbBorderColor, defaultThumbColor);
            upperThumbBorderColor = GetColorOrDefault(upperThumbBorderColor, defaultThumbColor);
        }

        LowerThumb.Stroke = lowerThumbBorderColor;
        UpperThumb.Stroke = upperThumbBorderColor;
        LowerThumb.BackgroundColor = GetColorOrDefault(lowerThumbColor, Colors.White);
        UpperThumb.BackgroundColor = GetColorOrDefault(upperThumbColor, Colors.White);
        Track.BackgroundColor = GetColorOrDefault(TrackColor, Color.FromRgb(182, 182, 182));
        TrackHighlight.BackgroundColor = GetColorOrDefault(TrackHighlightColor, Color.FromRgb(46, 124, 246));
        Track.Stroke = GetColorOrDefault(TrackBorderColor, null);
        TrackHighlight.Stroke = GetColorOrDefault(TrackHighlightBorderColor, null);

        var trackSize = TrackSize;
        var trackRadius = (float)GetDoubleOrDefault(TrackRadius, trackSize / 2);
        var lowerThumbSize = GetDoubleOrDefault(LowerThumbSize, ThumbSize);
        var upperThumbSize = GetDoubleOrDefault(UpperThumbSize, ThumbSize);
        var trackCornerRadius = GetDoubleOrDefault(TrackCornerRadius, TrackSize / 2);
        var trackHighlightCornerRadius = GetDoubleOrDefault(TrackHighlightCornerRadius, TrackSize / 2);

        Track.StrokeShape = new RoundRectangle
        {
            CornerRadius = new CornerRadius(trackCornerRadius)
        };

        TrackHighlight.StrokeShape = new RoundRectangle
        {
            CornerRadius = new CornerRadius(trackHighlightCornerRadius)
        };

        // Apply Background Colors
        Track.BackgroundColor = TrackBackgroundColor;
        TrackHighlight.BackgroundColor = TrackHighlightBackgroundColor;
        var lowerCornerRadius = GetDoubleOrDefault(GetDoubleOrDefault(LowerThumbCornerRadius, ThumbCornerRadius), 10);
        var upperCornerRadius = GetDoubleOrDefault(GetDoubleOrDefault(UpperThumbCornerRadius, ThumbCornerRadius), 10);

        LowerThumb.StrokeShape = new RoundRectangle
        {
            CornerRadius = new CornerRadius(lowerCornerRadius)
        };

        UpperThumb.StrokeShape = new RoundRectangle
        {
            CornerRadius = new CornerRadius(upperCornerRadius)
        };
        LowerThumb.Content = LowerThumbView;
        UpperThumb.Content = UpperThumbView;

        var labelWithSpacingHeight = Max(Max(LowerValueLabel.Height, UpperValueLabel.Height), 0);
        if (labelWithSpacingHeight > 0)
            labelWithSpacingHeight += ValueLabelSpacing;

        var trackThumbHeight = Max(Max(lowerThumbSize, upperThumbSize), trackSize);
        var trackVerticalPosition = labelWithSpacingHeight + ((trackThumbHeight - trackSize) / 2);
        var lowerThumbVerticalPosition = labelWithSpacingHeight + ((trackThumbHeight - lowerThumbSize) / 2);
        var upperThumbVerticalPosition = labelWithSpacingHeight + ((trackThumbHeight - upperThumbSize) / 2);

        if (Control != null)
            Control.HeightRequest = labelWithSpacingHeight + trackThumbHeight;

        var trackHighlightBounds = GetLayoutBounds(TrackHighlight);
        SetLayoutBounds(TrackHighlight, new Rect(trackHighlightBounds.X, trackVerticalPosition, trackHighlightBounds.Width, trackSize));
        SetLayoutBounds(Track, new Rect(0, trackVerticalPosition, Width, trackSize));
        SetLayoutBounds(LowerThumb, new Rect(0, lowerThumbVerticalPosition, lowerThumbSize, lowerThumbSize));
        SetLayoutBounds(UpperThumb, new Rect(0, upperThumbVerticalPosition, upperThumbSize, upperThumbSize));
        SetLayoutBounds(LowerValueLabel, new Rect(0, 0, -1, -1));
        SetLayoutBounds(UpperValueLabel, new Rect(0, 0, -1, -1));
        SetValueLabelBinding(LowerValueLabel, LowerValueProperty);
        SetValueLabelBinding(UpperValueLabel, UpperValueProperty);
        LowerValueLabel.Style = LowerValueLabelStyle ?? ValueLabelStyle;
        UpperValueLabel.Style = UpperValueLabelStyle ?? ValueLabelStyle;

        OnLowerUpperValuePropertyChanged();

        Track.BatchCommit();
        TrackHighlight.BatchCommit();
        LowerThumb.BatchCommit();
        UpperThumb.BatchCommit();
        LowerValueLabel.BatchCommit();
        UpperValueLabel.BatchCommit();
        BatchCommit();
    }

    void OnViewSizeChanged(object? sender, System.EventArgs e)
    {
        var maxHeight = Max(LowerValueLabel.Height, UpperValueLabel.Height);
        if ((sender == LowerValueLabel || sender == UpperValueLabel) && labelMaxHeight == maxHeight)
        {
            if (Dispatcher.IsDispatchRequired)
            {
                Dispatcher.Dispatch(OnValueLabelTranslationChanged);
                return;
            }
            OnValueLabelTranslationChanged();
            return;
        }

        labelMaxHeight = maxHeight;
        OnLayoutPropertyChanged();
    }

    void OnPanUpdated(object? sender, PanUpdatedEventArgs e)
    {
        var view = (View)(sender ?? throw new NullReferenceException($"{nameof(sender)} cannot be null"));

        switch (e.StatusType)
        {
            case GestureStatus.Started:
                OnPanStarted(view);
                break;
            case GestureStatus.Running:
                OnPanRunning(view, e.TotalX);
                break;
            case GestureStatus.Completed:
            case GestureStatus.Canceled:
                OnPanCompleted(view);
                break;
        }
    }

    void OnPanStarted(View view)
    {
        thumbPositionMap[view] = view.TranslationX;
        RaiseEvent(view == LowerThumb
            ? LowerDragStarted
            : UpperDragStarted);

        if (Interlocked.Increment(ref dragCount) == 1)
            RaiseEvent(DragStarted);
    }

    void OnPanRunning(View view, double value)
        => UpdateValue(view, value + GetPanShiftValue(view));

    void OnPanCompleted(View view)
    {
        thumbPositionMap[view] = view.TranslationX;
        RaiseEvent(view == LowerThumb
            ? LowerDragCompleted
            : UpperDragCompleted);

        if (Interlocked.Decrement(ref dragCount) == 0)
            RaiseEvent(DragCompleted);
    }

    void UpdateValue(View view, double value)
    {
        var rangeValue = MaximumValue - MinimumValue;
        if (view == LowerThumb)
        {
            LowerValue = Min(Max(MinimumValue, (value / TrackWidth * rangeValue) + MinimumValue), UpperValue);
            return;
        }
        UpperValue = Min(Max(LowerValue, ((value - LowerThumb.Width) / TrackWidth * rangeValue) + MinimumValue), MaximumValue);
    }

    double GetPanShiftValue(View view)
        => DeviceInfo.Platform == DevicePlatform.Android
            ? view.TranslationX
            : thumbPositionMap[view];

    void SetValueLabelBinding(Label label, BindableProperty bindableProperty)
        => label.SetBinding(Label.TextProperty, new Binding
        {
            Source = this,
            Path = bindableProperty.PropertyName,
            StringFormat = ValueLabelStringFormat
        });

    void AddGestureRecognizer(View view, PanGestureRecognizer gestureRecognizer)
    {
        gestureRecognizer.PanUpdated += OnPanUpdated;
        view.GestureRecognizers.Add(gestureRecognizer);
    }

    Color? GetColorOrDefault(Color? color, Color? defaultColor)
        => color ?? defaultColor;

    double GetDoubleOrDefault(double value, double defaultSize)
        => value < 0
            ? defaultSize
            : value;

    void RaiseEvent(EventHandler? eventHandler)
        => eventHandler?.Invoke(this, EventArgs.Empty);
}

sealed class ThumbBorder : Border
{
    public ThumbBorder() 
    {
    }
}
