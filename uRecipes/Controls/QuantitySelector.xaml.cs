using System;

namespace uRecipes.Controls;

public partial class QuantitySelector : ContentView
{
	public static readonly BindableProperty QuantityProperty =
		BindableProperty.Create(nameof(Quantity), 
			typeof(int), 
			typeof(QuantitySelector), 
			defaultValue: 1,
			defaultBindingMode: BindingMode.TwoWay,
			propertyChanged: (bindable, oldvalue, newvalue) =>
		{
			var control = (QuantitySelector)bindable;
			control.QuantityLabel.Text = newvalue?.ToString();
		});

	public int Quantity 
	{
		get => (int) GetValue(QuantityProperty); 
		set => SetValue(QuantityProperty, (int) value);
	}
	public QuantitySelector()
	{
		InitializeComponent();
	}

	private void OnPlusTaped(object sender, TappedEventArgs e) => Quantity++;

    private void OnMinusTaped(object sender, TappedEventArgs e)
    {
		if (Quantity == 1)
			return;

        Quantity--;
    }
}