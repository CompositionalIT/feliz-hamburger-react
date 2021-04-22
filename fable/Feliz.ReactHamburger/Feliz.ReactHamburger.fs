module Feliz.ReactHamburger

open Feliz
open Fable.Core.JsInterop

let hamburger: obj = importDefault "hamburger-react"
let squash: obj = import "Squash" "hamburger-react"
let cross: obj = import "Cross" "hamburger-react"
let twirl: obj = import "Twirl" "hamburger-react"
let fade: obj = import "Fade" "hamburger-react"
let slant: obj = import "Slant" "hamburger-react"
let spiral: obj = import "Spiral" "hamburger-react"
let divide: obj = import "Divide" "hamburger-react"
let turn: obj = import "Turn" "hamburger-react"
let pivot: obj = import "Pivot" "hamburger-react"
let sling: obj = import "Sling" "hamburger-react"
let squeeze: obj = import "Squeeze" "hamburger-react"
let spin: obj = import "Spin" "hamburger-react"
let rotate: obj = import "Rotate" "hamburger-react"

type Direction =
    | Left
    | Right

type Hamburger =
    | Tilt
    | Squash
    | Cross
    | Twirl
    | Fade
    | Slant
    | Spiral
    | Divide
    | Turn
    | Pivot
    | Sling
    | Squeeze
    | Spin
    | Rotate
        with static member importLib = function
                | Tilt -> hamburger
                | Squash -> squash
                | Cross -> cross
                | Twirl -> twirl
                | Fade -> fade
                | Slant -> slant
                | Spiral -> spiral
                | Divide -> divide
                | Spin -> spin
                | Rotate -> rotate
                | Squeeze -> squeeze
                | Sling -> sling
                | Pivot -> pivot
                | Turn -> turn


type LineDistance =
    | Small
    | Medium
    | Large
        with static member toPropValue = function
                    | Small -> "sm"
                    | Large -> "lg"
                    | Medium -> "md"

type ReactHamburger =
    static member inline toggled (v: bool) = "toggled" ==> v
    static member inline size (s: int) = "toggled" ==> s
    static member inline direction (d: Direction) = "direction" ==> (d.ToString().ToLower())
    static member inline duration (t: float) = "duration" ==> t
    static member inline lineDistance (d: LineDistance) = "distance" ==> (LineDistance.toPropValue d)
    static member inline toggle (f) = "toggle" ==> f
    static member inline color (s: string) = "color" ==> s
    static member inline easing (s: string) = "easing" ==> s
    static member inline onToggle (f) = "onToggle" ==> f
    static member inline rounded (b: bool) = "rounded" ==> b
    static member inline hideOutline (b: bool) = "hideOutline" ==> b

    static member inline hamburgerType (h: Hamburger) = "hamburgerType" ==> (h |> Hamburger.importLib)
    static member create props =
        let givenProps = !!props

        let importType =
            givenProps
            |> Seq.tryFind (fst >> (=) "hamburgerType")
            |> function
                | Some x -> snd x
                | None -> hamburger

        Interop.reactApi.createElement (importType, createObj !!props)