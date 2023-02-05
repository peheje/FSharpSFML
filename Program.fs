open SFML.Graphics
open SFML.Window
open System
open SFML.System

let onClose (sender: Object) (e: EventArgs) =
    let window = sender :?> Window
    window.Close()

let settings = ContextSettings(8u, 8u, 8u)

let window =
    new RenderWindow(new VideoMode(640u, 480u), "This is it", Styles.Default, settings)

type Circle(rw: RenderWindow) =

    let circle = new CircleShape(100.0f, 100u)
    do circle.FillColor <- Color.Green
    member o.Draw() = rw.Draw(circle)
    member o.Move(x, y) = circle.Position <- new Vector2f(x, y)

    member o.MoveLeft() =
        circle.Position <- new Vector2f(circle.Position.X - 4f, circle.Position.Y)

    member o.MoveRight() =
        circle.Position <- new Vector2f(circle.Position.X + 4f, circle.Position.Y)

    member o.Diameter = circle.Radius * 2f

    member o.Position = circle.Position

let circle = Circle(window)

window.SetActive() |> ignore
window.Closed.AddHandler(onClose)

type Direction =
    | Left
    | Right

let mutable direction = Right

while window.IsOpen do
    Threading.Thread.Sleep(10)
    window.Clear()
    window.DispatchEvents()

    if direction = Left then
        circle.MoveLeft()

        if circle.Position.X < 0f then
            printfn "Go right"
            direction <- Right
    else
        circle.MoveRight()

        if circle.Position.X + circle.Diameter > 640f then
            printfn "Go left"
            direction <- Left

    circle.Draw()

    window.Display()
