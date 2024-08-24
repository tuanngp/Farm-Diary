using SkiaSharp;
using SkiaSharp.Views.Maui;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FarmDiary;

public partial class HomePage : ContentPage
{
	public ObservableCollection<GreenhouseItem> GreenhouseItems { get; set; }
	public Command<GreenhouseItem> SelectGreenhouseCommand { get; set; }
	public HomePage()
	{
		InitializeComponent();
		// InitializeGreenhouseItems();
		GreenhouseItems = new ObservableCollection<GreenhouseItem>()
		{
			new GreenhouseItem { Name = "A", Color = Color.FromHex("#4CAF50") },
			new GreenhouseItem { Name = "B", Color = Color.FromHex("#2196F3") },
			new GreenhouseItem { Name = "C", Color = Color.FromHex("#FFC107") },
			new GreenhouseItem { Name = "D", Color = Color.FromHex("#9C27B0") },
			new GreenhouseItem { Name = "E", Color = Color.FromHex("#FF5722") }
		};
		InitializeCommands();
		BindingContext = this;
	}

	private void InitializeGreenhouseItems()
	{
		GreenhouseItems = new ObservableCollection<GreenhouseItem>()
		{
			new GreenhouseItem { Name = "A", Color = Color.FromHex("#4CAF50") },
			new GreenhouseItem { Name = "B", Color = Color.FromHex("#2196F3") },
			new GreenhouseItem { Name = "C", Color = Color.FromHex("#FFC107") },
			new GreenhouseItem { Name = "D", Color = Color.FromHex("#9C27B0") },
			new GreenhouseItem { Name = "E", Color = Color.FromHex("#FF5722") }
		};
	}
	private void InitializeCommands()
	{
		SelectGreenhouseCommand = new Command<GreenhouseItem>(OnGreenhouseSelected);
	}

	private void OnGreenhouseSelected(GreenhouseItem selectedGreenhouse)
	{
		// Handle the greenhouse selection here
		// For example, you could navigate to a detail page or update some UI
		DisplayAlert("Greenhouse Selected", $"You selected greenhouse {selectedGreenhouse.Name}", "OK");
	}

	private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
	{
		SKImageInfo info = args.Info;
		SKSurface surface = args.Surface;
		SKCanvas canvas = surface.Canvas;
	
		canvas.Clear(SKColors.White);
	
		float width = info.Width;
		float height = info.Height;
		float padding = 50;
		float chartWidth = width - (2 * padding);
		float chartHeight = height - (2 * padding);
	
		// Dữ liệu mẫu
		float[] dataPoints = { 50, 80, 60, 100, 70, 90, 75, 110, 95, 120, 85, 105 };
		string[] months = { "T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9", "T10", "T11", "T12" };
	
		using (SKPaint linePaint = new SKPaint())
		using (SKPaint fillPaint = new SKPaint())
		using (SKPaint textPaint = new SKPaint())
		using (SKPaint axisPaint = new SKPaint())
		{
			linePaint.Color = SKColors.Blue;
			linePaint.StrokeWidth = 2;
			linePaint.IsAntialias = true;
			linePaint.Style = SKPaintStyle.Stroke;
	
			fillPaint.Color = linePaint.Color.WithAlpha(50);
			fillPaint.Style = SKPaintStyle.Fill;
	
			textPaint.Color = SKColors.Black;
			textPaint.TextSize = 12;
			textPaint.IsAntialias = true;
	
			axisPaint.Color = SKColors.Black;
			axisPaint.StrokeWidth = 1;
			axisPaint.IsAntialias = true;
	
			// Draw axes
			canvas.DrawLine(padding, height - padding, width - padding, height - padding, axisPaint);
			canvas.DrawLine(padding, padding, padding, height - padding, axisPaint);
	
			float xStep = chartWidth / (dataPoints.Length - 1);
			float yScale = chartHeight / 120f;
	
			SKPath path = new SKPath();
			path.MoveTo(padding, height - padding - dataPoints[0] * yScale);
	
			for (int i = 0; i < dataPoints.Length; i++)
			{
				float x = padding + i * xStep;
				float y = height - padding - dataPoints[i] * yScale;
	
				if (i > 0) path.LineTo(x, y);
	
				// Draw data point
				canvas.DrawCircle(x, y, 4, linePaint);
	
				// Draw x-axis label
				canvas.DrawText(months[i], x - 10, height - padding + 20, textPaint);
	
				// Draw y-axis gridline
				canvas.DrawLine(padding - 5, y, width - padding, y, new SKPaint { Color = SKColors.LightGray, StrokeWidth = 1 });
			}
	
			// Draw line
			canvas.DrawPath(path, linePaint);
	
			// Fill area under the line
			path.LineTo(width - padding, height - padding);
			path.LineTo(padding, height - padding);
			path.Close();
			canvas.DrawPath(path, fillPaint);
	
			// Draw y-axis labels
			for (int i = 0; i <= 6; i++)
			{
				float y = height - padding - i * (chartHeight / 6);
				canvas.DrawText((i * 20).ToString(), 5, y + 5, textPaint);
			}
	
			// Draw chart title
			using (SKPaint titlePaint = new SKPaint { Color = SKColors.Black, TextSize = 16, TextAlign = SKTextAlign.Center })
			{
				canvas.DrawText("Biểu đồ dữ liệu theo tháng", width / 2, padding / 2, titlePaint);
			}
		}
	}
}