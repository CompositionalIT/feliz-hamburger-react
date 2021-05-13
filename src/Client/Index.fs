module Index

open Elmish
open Fable.Remoting.Client
open Shared

type Model = { IsToggled: bool }

type Msg =
    | Toggle

let todosApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<ITodosApi>

let init () : Model * Cmd<Msg> =
    let model = { IsToggled = false }

    model, Cmd.none

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | Toggle -> { model with IsToggled = not model.IsToggled }, Cmd.none

open Feliz
open Feliz.ReactHamburger
open Feliz.Bulma

let navBrand =
    Bulma.navbarBrand.div [
        Bulma.navbarItem.a [
            prop.href "https://safe-stack.github.io/"
            navbarItem.isActive
            prop.children [
                Html.img [
                    prop.src "/favicon.png"
                    prop.alt "Logo"
                ]
            ]
        ]
    ]



let view (model: Model) (dispatch: Msg -> unit) =
    Bulma.hero [
        hero.isFullHeight
        color.isPrimary
        prop.style [
            style.backgroundSize "cover"
            style.backgroundImageUrl "https://unsplash.it/1200/900?random"
            style.backgroundPosition "no-repeat center center fixed"
        ]
        prop.children [
            Bulma.heroHead [
                Bulma.navbar [
                    Bulma.container [ navBrand ]
                ]
            ]
            Bulma.heroBody [
                Bulma.container [
                    Bulma.column [
                        column.is6
                        column.isOffset3
                        prop.children [
                            Bulma.title [
                                text.hasTextCentered
                                prop.text "hamburger_react"
                            ]
                            ReactHamburger.create [
                                ReactHamburger.rounded true
                                ReactHamburger.hamburgerType Squash
                                ReactHamburger.toggled model.IsToggled
                                ReactHamburger.toggle (fun () -> Toggle |> dispatch)
                            ]
                        ]
                    ]
                ]
            ]
        ]
    ]
