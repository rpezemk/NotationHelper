using Mono.Terminal;
using static Terminal.Gui.View;
using Terminal.Gui;
using AudioTool.Trivial;
using AudioTool.Flat;
using AudioTool.Layered;
using AudioTool.Helpers;
using AudioTool.Total;

class Program
{
    static void Main()
    {
        BackgroundCsThread(null, (s) => { });
        //RunTUI();
    }
    static void BackgroundCsThread(string[] args, Action<string> LogAction)
    {
        var noise = new NoiseInstrument("NOISE");
        var reverb = new ReverbInstrument("REVERB");
        var flatViola = new FlatSampleInstrument("VIOLA_01");
        var flatViolin = new FlatSampleInstrument("VIOLIN_01");
        //var layeredViola = new LayerSampleInstrument("VIOLA_01", InstrumentFileHelper.ViolaLayers);
        var layeredViola = new TotalLayerSampleInstrument("VIOLA_01", InstrumentFileHelper.TotalViolaLayers);
        var layeredViolin = new TotalLayerSampleInstrument("VIOLIN_01", InstrumentFileHelper.TotalViolinLayers);
        var layeredCellos = new TotalLayerSampleInstrument("VIOLIN_01", InstrumentFileHelper.TotalVioloncelliLayers);
        var layeredBasses = new TotalLayerSampleInstrument("VIOLIN_01", InstrumentFileHelper.TotalContrabbassiLayers);
        CsEngine csEngine = new CsEngine([noise, reverb, flatViola, layeredViola, layeredViolin, layeredCellos, layeredBasses]);
        csEngine.RunAsync();
        var pitches = new List<(int, double)>() 
        {
            //(6,  1.00),
            (2,  0.125*2),
            (4,  0.125*2),
            (5,  0.125*2),
            (7,  0.125*2),
            (9,  0.250*2),
            (14, 0.250*2),
            (13, 0.250*2),
            (9,  0.250*2),
            (4,  0.250*2),
            (7,  0.250*2),
            (6,  0.250*2),
            (2,  0.250*2),
            (12, 0.750*2),
            (11, 0.125*2),
            (9,  0.125*2),
            (11, 0.250*2),
            (7,  0.250*2),
        };
        Thread.Sleep(1000);
        var noteDuration = 2;
        var pause = 0;
        bool legato = true;
        Task.Run(() =>
        {
            while (true)
            {
                foreach (var p in pitches)
                {
                    layeredBasses.PlaySample(p.Item1, p.Item2+0.2, legato);
                    //layeredViola.PlaySeparatedNote(p + 7, 8);
                    //layeredViola.PlaySeparatedNote(p + 16, 8);
                    Thread.Sleep((int)(p.Item2 * 1000));
                    //Thread.Sleep(2000);
                }
            }
        });

        var dynamicsDelay = 5;//s
        var dynamics = 1.0;
        Task.Run(() =>
        {
            while (false)
            {
                dynamics = 1;
                layeredViola.ApplyDynamics(dynamicsDelay, dynamics);
                Thread.Sleep(dynamicsDelay * 1000);
                dynamics = 0.2;
                layeredViola.ApplyDynamics(dynamicsDelay, dynamics);
                Thread.Sleep(dynamicsDelay * 1000);
            }
        });


        Thread.Sleep(3600 * 1000);
    }

    private static void RunTUI()
    {
        Application.Init();

        var top = Application.Top;

        // Create a custom color scheme with a black background for the left pane
        var scheme = new ColorScheme()
        {
            Normal = Application.Driver.MakeAttribute(Color.White, Color.Black),  // White text on black background
            Focus = Application.Driver.MakeAttribute(Color.White, Color.Black), // Focused state
            HotNormal = Application.Driver.MakeAttribute(Color.BrightYellow, Color.Black), // Hot state (e.g., for buttons)
            HotFocus = Application.Driver.MakeAttribute(Color.BrightGreen, Color.Black) // Hot and Focused state
        };

        // Create a Toplevel instead of a Window to avoid borders
        var win = new Toplevel() { X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };
        top.Add(win);

        var toolbar = new FrameView("Toolbar") { X = 0, Y = 0, Width = Dim.Fill(), Height = 3, ColorScheme = scheme, };
        win.Add(toolbar);

        // Add buttons to the toolbar
        var saveButton = new Button("Save") { X = 1, Y = 0, Width = 10, ColorScheme = scheme };
        var loadButton = new Button("Load") { X = Pos.Right(saveButton) + 1, Y = 0, Width = 10, ColorScheme = scheme };
        var clearButton = new Button("Load") { X = Pos.Right(loadButton) + 1, Y = 0, Width = 10, ColorScheme = scheme };
        var runBackButton = new Button("runBackButton") { X = Pos.Right(clearButton) + 1, Y = 0, Width = 10, ColorScheme = scheme };
        toolbar.Add(saveButton, loadButton, clearButton, runBackButton);

        saveButton.Clicked += () => MessageBox.Query("Save", "Save functionality not implemented yet.", "Ok");
        loadButton.Clicked += () => MessageBox.Query("Save", "Save functionality not implemented yet.", "Ok");
        clearButton.Clicked += () => MessageBox.Query("Save", "Save functionality not implemented yet.", "Ok");

        var leftPane = new FrameView("Text Editor") { X = 0, Y = Pos.Bottom(toolbar), Width = Dim.Percent(50), Height = Dim.Fill(), ColorScheme = scheme };
        win.Add(leftPane);

        var textView = new TextView() { X = 1, Y = 1, Width = Dim.Fill(), Height = Dim.Fill(), ColorScheme = scheme, };
        leftPane.Add(textView);
        textView.Text = "abcd";
        runBackButton.Clicked += () => { Task.Run(() => BackgroundCsThread(null, s => textView.Text += "\n" + s)); };
        // Right pane for controls
        var rightPane = new FrameView("Controls") { X = Pos.Right(leftPane), Y = Pos.Bottom(toolbar), Width = Dim.Fill(), Height = Dim.Fill(), };
        win.Add(rightPane);

        // Add a button to retrieve text from the text editor
        var retrieveButton = new Button("Retrieve Text")
        {
            X = Pos.Center(),
            Y = Pos.Center()
        };
        rightPane.Add(retrieveButton);

        // Show the text from the TextView when the button is clicked
        retrieveButton.Clicked += () =>
        {
            string textContent = textView.Text.ToString();
            MessageBox.Query("Text Retrieved", textContent, "Ok"); // Display retrieved text
        };

        Application.Run();
    }

}
