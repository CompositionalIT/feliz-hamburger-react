namespace App

open Feliz
open Feliz.Router
open Fable.React.Props
open Fable.Core.JsInterop
open Feliz.ReactHamburger


type StyledComponents =
    static member Row (children: ReactElement seq) =
        Html.div [
            prop.style [
                style.alignItems.center
                style.display.flex
            ]
            prop.children children
        ]

type Props =
    { Toggled: bool
      Direction: Direction
      HideOutLine: bool
      LineDistance: LineDistance
      Animation: Hamburger
      Rounded: bool }

type Components =

    static member citLightBlue = "#40a8b7"
    static member citGreen = "#8cbf41"
    static member citYellow = "#fec903"
    static member citRed = "#e1053a"
    static member citOrange = "#e97305"
    static member citDarkBlue = "#102035"

    [<ReactComponent>]
    static member Navbar (wrapperName: string) =
        let logo: obj = importDefault "./cit-logo.png"
        Html.div [
            prop.style [
                style.padding (0, 20)
                style.backgroundColor Components.citDarkBlue
                style.height 70
                style.color "white"
                style.display.flex
                style.justifyContent.spaceBetween
                style.alignItems.center
            ]
            prop.children [
                StyledComponents.Row [
                    Html.img [
                        prop.style [
                            style.height 50
                        ]
                        prop.src (unbox<string>logo)
                    ]
                    Html.h1 "Compositional IT"
                ]
                Html.div wrapperName
            ]
        ]


    [<ReactComponent>]
    static member Demo () =
        let initProps =
            { Toggled = false
              Direction = Left
              HideOutLine = true
              Animation = Tilt
              LineDistance = Small
              Rounded = true }

        let (props, setProps) = React.useState(initProps)

        let animationButtonConfigs =
            [ {| Color = Components.citLightBlue
                 Hamburger = "Tilt"
                 Updater = (fun _ -> setProps({ props with Animation = Tilt }))
                 Selected = props.Animation = Tilt |}

              {| Color = Components.citGreen
                 Hamburger = "Squash"
                 Updater = (fun _ -> setProps({ props with Animation = Squash }))
                 Selected = props.Animation = Squash |}

              {| Color = Components.citOrange
                 Hamburger = "Cross"
                 Updater = (fun _ -> setProps({ props with Animation = Cross }))
                 Selected = props.Animation = Cross |}

              {| Color = Components.citYellow
                 Hamburger = "Twirl"
                 Updater = (fun _ -> setProps({ props with Animation = Twirl }))
                 Selected = props.Animation = Twirl |}

              {| Color = Components.citRed
                 Hamburger = "Fade"
                 Updater = (fun _ -> setProps({ props with Animation = Fade }))
                 Selected = props.Animation = Fade |}

              {| Color = Components.citLightBlue
                 Hamburger = "Slant"
                 Updater = (fun _ -> setProps({ props with Animation = Slant }))
                 Selected = props.Animation = Slant |}

              {| Color = Components.citYellow
                 Hamburger = "Divide"
                 Updater = (fun _ -> setProps({ props with Animation = Divide }))
                 Selected = props.Animation = Divide |}

              {| Color = Components.citGreen
                 Hamburger = "Pivot"
                 Updater = (fun _ -> setProps({ props with Animation = Pivot }))
                 Selected = props.Animation = Pivot |}

              {| Color = Components.citOrange
                 Hamburger = "Turn"
                 Updater = (fun _ -> setProps({ props with Animation = Turn }))
                 Selected = props.Animation = Turn |}

              {| Color = Components.citYellow
                 Hamburger = "Sling"
                 Updater = (fun _ -> setProps({ props with Animation = Sling }))
                 Selected = props.Animation = Sling |}

              {| Color = Components.citRed
                 Hamburger = "Squeeze"
                 Updater = (fun _ -> setProps({ props with Animation = Squeeze }))
                 Selected = props.Animation = Squeeze |}

              {| Color = Components.citLightBlue
                 Hamburger = "Spin"
                 Updater = (fun _ -> setProps({ props with Animation = Spin }))
                 Selected = props.Animation = Spin |}

              {| Color = Components.citGreen
                 Hamburger = "Rotate"
                 Updater = (fun _ -> setProps({ props with Animation = Rotate }))
                 Selected = props.Animation = Rotate |} ]

        let citButton color updateProp selected =
            Html.button [
                prop.style [
                    style.backgroundColor (if selected then color else "grey")
                    style.padding 10
                    style.border(1, borderStyle.none, "")
                    style.borderRadius 50
                    style.height 30
                    style.width 30
                ]
                prop.onClick updateProp
            ]

        let toggleButton color (name: string) updater selected =
            Html.div [
                prop.style [ style.display.flex; style.justifyContent.spaceBetween; style.alignItems.center; style.marginBottom 10 ]
                prop.children [
                    Html.p name
                    citButton color updater selected
                ]
            ]

        let switchButton color (buttonLabel: string) updater selected =
            Html.button [
                prop.style [
                    style.backgroundColor (if selected then color else "transparent")
                    style.color (if selected then "white" else Components.citDarkBlue)
                    style.border(1,borderStyle.none, "")
                    style.borderRight(2, borderStyle.solid, color)
                    style.padding 8
                    style.width 70
                    style.height (length.percent 100)
                    style.margin 0
                ]
                prop.text buttonLabel
                prop.onClick updater
            ]


        let switchButtons color (groupingName: string) (buttonConfig: {| Name: string; Updater: Browser.Types.MouseEvent -> unit; Selected: bool |} list) =
            Html.div [
                prop.style [ style.display.flex; style.justifyContent.spaceBetween; style.alignItems.center; style.marginBottom 10 ]
                prop.children [
                    Html.div groupingName
                    Html.div [
                        prop.style [
                            style.border(2, borderStyle.solid, color)
                            style.borderRight(2, borderStyle.none, color)
                            style.borderRadius 5
                        ]
                        prop.children [
                            for button in buttonConfig do
                                switchButton color button.Name button.Updater button.Selected
                        ]
                    ]
                ]
            ]

        let defaultButton color (buttonLabel: string) updater selected =
            Html.button [
                prop.style [
                    style.width (length.percent 100)
                    style.backgroundColor (if selected then color else "transparent")
                    style.color (if selected then "white" else Components.citDarkBlue)
                    style.border(2,borderStyle.solid, color)
                    style.borderRadius 5
                    style.padding 8
                    style.height 50
                    style.fontSize 15
                    style.marginBottom 10
                ]
                prop.text buttonLabel
                prop.onClick updater
            ]

        let subHeading (label: string) =
            Html.h2 [
                prop.style [
                    style.borderBottom(2, borderStyle.solid, Components.citDarkBlue)
                    style.paddingBottom 10
                ]
                prop.text label
            ]

        let codeBlock (code: string) =
            Html.pre [
                prop.style [
                    style.padding 20
                    style.fontSize 15
                    style.backgroundColor "lightgrey"
                    style.borderRadius 5
                ]
                prop.text code
            ]

        Html.div [
            prop.style [
                style.display.flex
                style.flexDirection.column
                style.padding 50
                //style.maxWidth 1000
                style.margin (0,length.auto)
            ]
            prop.children [
                Html.div [
                    prop.style [ style.display.flex; style.flexWrap.wrap ]
                    prop.children [
                        Html.div [
                            prop.style [
                                style.flexGrow 1

                            ]
                            prop.children [
                                subHeading "Demo"
                                ReactHamburger.create [
                                    ReactHamburger.hamburgerType props.Animation
                                    ReactHamburger.direction props.Direction
                                    ReactHamburger.toggled props.Toggled
                                    ReactHamburger.hideOutline props.HideOutLine
                                    ReactHamburger.rounded props.Rounded
                                    ReactHamburger.lineDistance props.LineDistance
                                ]

                                subHeading "Installation"
                                codeBlock
                                        """cd ./project
femto install Feliz.ReactHamburger"""

                                subHeading "Sample Code"
                                codeBlock
                                    """
ReactHamburger.create [
    ReactHamburger.hamburgerType Tilt
    ReactHamburger.direction Left
    ReactHamburger.toggled true
    ReactHamburger.hideOutline false
    ReactHamburger.rounded false
    ReactHamburger.lineDistance Small
    ]
                                    """

                            ]
                        ]
                        Html.div [
                            prop.style [
                                style.flexGrow 1
                                style.padding (0, 20)
                            ]
                            prop.children [
                                Html.div [
                                    prop.children [
                                        subHeading "Boolean Props"
                                        toggleButton
                                            Components.citLightBlue
                                            "Toggled"
                                            (fun _ -> setProps({ props with Toggled = not props.Toggled }))
                                            props.Toggled
                                        toggleButton
                                            Components.citRed
                                            "Rounded"
                                            (fun _ -> setProps({ props with Rounded = not props.Rounded }))
                                            props.Rounded
                                        toggleButton
                                            Components.citGreen
                                            "Hide outline"
                                            (fun _ -> setProps({ props with HideOutLine = not props.HideOutLine }))
                                            props.HideOutLine

                                        subHeading "DU Props"
                                        switchButtons
                                            Components.citYellow
                                            "Direction"
                                            [ {| Name = "Left"; Updater = (fun _ -> setProps({ props with Direction = Left})); Selected = (props.Direction = Left) |}
                                              {| Name = "Right"; Updater = (fun _ -> setProps({ props with Direction = Right})); Selected = (props.Direction = Right) |}]

                                        switchButtons
                                            Components.citOrange
                                            "Distance"
                                            [ {| Name = "Small"; Updater = (fun _ -> setProps({ props with LineDistance = Small })); Selected = (props.LineDistance = Small) |}
                                              {| Name = "Medium"; Updater = (fun _ -> setProps({ props with LineDistance = Medium })); Selected = (props.LineDistance = Medium) |}
                                              {| Name = "Large"; Updater = (fun _ -> setProps({ props with LineDistance = Large })); Selected = (props.LineDistance = Large)|}]

                                        subHeading "HamburgerType Prop"
                                        for animationButton in animationButtonConfigs do
                                            defaultButton animationButton.Color animationButton.Hamburger animationButton.Updater animationButton.Selected

                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]

    [<ReactComponent>]
    static member Footer () =
        Html.div [
            prop.style [
                style.backgroundColor "#102035"
                style.height 300
                style.color "white"
                style.display.flex
                style.justifyContent.spaceBetween
                style.alignItems.center
                style.position.relative
            ]
            prop.children [

            ]
        ]


    [<ReactComponent>]
    static member Documentation () =
        Html.div [
            Components.Navbar("Feliz.ReactHamburger")
            Components.Demo()
            Components.Footer()
        ]

