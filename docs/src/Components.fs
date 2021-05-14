namespace App

open Feliz
open Fable.Core.JsInterop
open Feliz.ReactHamburger

type CitColors =
    static member lightBlue = "#40a8b7"
    static member green = "#8cbf41"
    static member yellow = "#fec903"
    static member red = "#e1053a"
    static member orange = "#e97305"
    static member darkBlue = "#102035"

type Package = { Name: string; Link: string }

type StyledComponents =

    static member Container (children: ReactElement list) =
        Html.div [
            prop.style [
                style.display.flex
                style.flexDirection.column
                style.padding 50
                style.maxWidth 1000
                style.margin (0,length.auto)
            ]
            prop.children children
        ]

    static member Row (children: ReactElement list) =
        Html.div [
            prop.style [
                style.alignItems.center
                style.display.flex
            ]
            prop.children children
        ]

    static member NavbarLink (label: string) link=
        Html.a [
            prop.style [ style.margin(20, 0, 20, 20); style.color "white"; style.fontSize 20; style.fontWeight.bold]
            prop.href link
            prop.text label
        ]


    static member Navbar (wrapperName: string) nugetPackage reactPackage =
        let logo: obj = importDefault "./cit-logo.png"
        Html.div [
            prop.style [
                style.padding (0, 20)
                style.backgroundColor CitColors.darkBlue
                style.height 70
                style.color "white"
                style.display.flex
                style.justifyContent.spaceBetween
                style.alignItems.center
            ]
            prop.children [
                Html.div [
                    prop.style [
                        style.width 1000
                        style.margin (0, length.auto)
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
                            Html.h2 $"Compositional IT - {wrapperName}"
                        ]
                        Html.div [
                            prop.style [ style.display.flex; style.justifyContent.spaceAround; style.alignItems.center ]
                            prop.children [
                                StyledComponents.NavbarLink
                                    nugetPackage.Name
                                    nugetPackage.Link
                                StyledComponents.NavbarLink
                                    reactPackage.Name
                                    reactPackage.Link
                            ]
                        ]
                    ]
                ]
            ]
        ]

    static member Description (wrapperName: string) =
         Html.h2 [
            prop.style [ style.marginBottom 70]
            prop.text $"Feliz bindings for {wrapperName}"
         ]
    static member SubHeading (label: string) =
        Html.h2 [
            prop.style [
                style.borderBottom(2, borderStyle.solid, CitColors.darkBlue)
                style.paddingBottom 10
            ]
            prop.text label
        ]

    static member HeadingWithContent (title: string) (children: ReactElement) =
        Html.div [
            StyledComponents.SubHeading title
            children
        ]

    static member CircleButton updateProp selected =
        Html.button [
            prop.style [
                style.backgroundColor (if selected then CitColors.lightBlue else "lightgrey")
                style.padding 10
                style.border(1, borderStyle.none, "")
                style.borderRadius 50
                style.height 30
                style.width 30
            ]
            prop.onClick updateProp
        ]

    static member LabelWithCircleButton (name: string) updater selected =
        Html.div [
            prop.style [ style.display.flex; style.justifyContent.spaceBetween; style.alignItems.center; style.marginBottom 10 ]
            prop.children [
                Html.b name
                StyledComponents.CircleButton updater selected
            ]
        ]

    static member OptionButton (buttonLabel: string) updater selected =
        Html.button [
            prop.style [
                style.backgroundColor (if selected then CitColors.green else "transparent")
                style.color (if selected then "white" else CitColors.darkBlue)
                style.border(1,borderStyle.none, "")
                style.borderRight(2, borderStyle.solid, CitColors.green)
                style.padding 8
                style.width 150
                style.height (length.percent 100)
                style.margin 0
                style.fontWeight.bold
            ]
            prop.text buttonLabel
            prop.onClick updater
        ]

    static member OptionButtons (groupingName: string) (buttonConfig: {| Name: string; Updater: Browser.Types.MouseEvent -> unit; Selected: bool |} list) =
        Html.div [
            prop.style [ style.display.flex; style.justifyContent.spaceBetween; style.alignItems.center; style.marginBottom 10; style.flexWrap.wrap ]
            prop.children [
                Html.b groupingName
                Html.div [
                    prop.style [
                        style.border(2, borderStyle.solid, CitColors.green)
                        style.borderRight(2, borderStyle.none, CitColors.green)
                        style.borderRadius 5
                    ]
                    prop.children [
                        for button in buttonConfig do
                            StyledComponents.OptionButton button.Name button.Updater button.Selected
                    ]
                ]
            ]
        ]

    static member  DefaultButton (buttonLabel: string) updater selected =
        Html.button [
            prop.style [
                style.width (length.percent 100)
                style.backgroundColor (if selected then CitColors.red else "transparent")
                style.color (if selected then "white" else CitColors.darkBlue)
                style.border(2, borderStyle.solid, CitColors.red)
                style.borderRadius 5
                style.padding 8
                style.height 50
                style.fontSize 15
                style.marginBottom 10
                style.fontWeight.bold
            ]
            prop.text buttonLabel
            prop.onClick updater
        ]

    static member CodeBlock (code: string) =
        Html.pre [
            prop.style [
                style.padding 20
                style.fontSize 15
                style.backgroundColor "lightgrey"
                style.borderRadius 5
            ]
            prop.text code
        ]

    static member Select (items: string list) (handler: Browser.Types.Event -> unit)=
        Html.select [
            prop.style [
                //style.backgroundColor (if selected then CitColors.red else "transparent")
                //style.color (if selected then "white" else CitColors.darkBlue)
                style.textAlign.center
                style.width 150
                style.border(2, borderStyle.solid, CitColors.green)
                style.borderRadius 5
                style.padding (0,8)
                style.height 50
                style.fontSize 15
                style.marginBottom 10
                style.fontWeight.bold
            ]
            prop.onChange handler
            prop.children [
                for item in items do
                    Html.option [
                        prop.style [ style.color CitColors.darkBlue]
                        prop.value item
                        prop.text item
                    ]
                ]
            ]

    static member LabelWithSelect (name: string) items handler =
        Html.div [
            prop.style [ style.display.flex; style.justifyContent.spaceBetween; style.alignItems.center; style.marginBottom 10 ]
            prop.children [
                Html.b name
                StyledComponents.Select items handler
            ]
        ]

    static member Footer (children: ReactElement list) =
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
            [ "Tilt"
              "Squash"
              "Cross"
              "Twirl"
              "Fade"
              "Slant"
              "Divide"
              "Pivot"
              "Turn"
              "Sling"
              "Squeeze"
              "Spin"
              "Rotate"
              ]

        let toHamburgerType = function
            | "Tilt" -> Tilt
            | "Squash" -> Squash
            | "Cross" -> Cross
            | "Twirl" -> Twirl
            | "Fade" -> Fade
            | "Slant" -> Slant
            | "Divide" -> Divide
            | "Pivot" -> Pivot
            | "Turn" -> Turn
            | "Sling" -> Sling
            | "Squeeze" -> Squeeze
            | "Spin" -> Spin
            | "Rotate" -> Rotate
            | _ -> failwith "invalid type"

        StyledComponents.Container [
            Html.div [
                prop.style [ style.display.flex; style.flexWrap.wrap; style.flexDirection.column ]
                prop.children [
                    Html.div [
                        StyledComponents.HeadingWithContent
                            "Demo"
                            (Html.div [
                                prop.style [ style.display.flex; style.justifyContent.center; style.alignItems.center ]
                                prop.children [
                                    ReactHamburger.create [
                                        ReactHamburger.hamburgerType props.Animation
                                        ReactHamburger.direction props.Direction
                                        ReactHamburger.size 100
                                        ReactHamburger.toggled props.Toggled
                                        ReactHamburger.hideOutline props.HideOutLine
                                        ReactHamburger.rounded props.Rounded
                                        ReactHamburger.lineDistance props.LineDistance
                                    ]
                                ]
                            ])
                        StyledComponents.HeadingWithContent
                            "Props"
                            (Html.div [
                                StyledComponents.LabelWithCircleButton
                                    "Toggled"
                                    (fun _ -> setProps({ props with Toggled = not props.Toggled }))
                                    props.Toggled
                                StyledComponents.LabelWithCircleButton
                                    "Rounded"
                                    (fun _ -> setProps({ props with Rounded = not props.Rounded }))
                                    props.Rounded
                                StyledComponents.LabelWithCircleButton
                                    "Hide outline"
                                    (fun _ -> setProps({ props with HideOutLine = not props.HideOutLine }))
                                    props.HideOutLine
                                StyledComponents.LabelWithSelect
                                    "HamburgerType"
                                    animationButtonConfigs
                                    (fun event -> setProps({ props with Animation = toHamburgerType event.target?value }))
                                StyledComponents.OptionButtons
                                    "Direction"
                                    [ {| Name = "Left"
                                         Updater = (fun _ -> setProps({ props with Direction = Left}))
                                         Selected = (props.Direction = Left) |}
                                      {| Name = "Right"
                                         Updater = (fun _ -> setProps({ props with Direction = Right}))
                                         Selected = (props.Direction = Right) |}]
                                StyledComponents.OptionButtons
                                    "Distance"
                                    [ {| Name = "Small"
                                         Updater = (fun _ -> setProps({ props with LineDistance = Small }))
                                         Selected = (props.LineDistance = Small) |}
                                      {| Name = "Medium"
                                         Updater = (fun _ -> setProps({ props with LineDistance = Medium }))
                                         Selected = (props.LineDistance = Medium) |}
                                      {| Name = "Large"
                                         Updater = (fun _ -> setProps({ props with LineDistance = Large }))
                                         Selected = (props.LineDistance = Large)|}]

                            ])


                    ]
                    Html.div [
                        Html.div [
                            prop.children [
                                StyledComponents.HeadingWithContent
                                    "Installation"
                                    (StyledComponents.CodeBlock
                                        """cd ./project
femto install Feliz.ReactHamburger""" )

                                StyledComponents.HeadingWithContent
                                    "Sample Code"
                                    (StyledComponents.CodeBlock
                                    """
ReactHamburger.create [
    ReactHamburger.hamburgerType Tilt
    ReactHamburger.direction Left
    ReactHamburger.toggled true
    ReactHamburger.hideOutline false
    ReactHamburger.rounded false
    ReactHamburger.lineDistance Small
]
                                    """ )
                            ]
                        ]
                    ]
                ]
            ]
        ]

    [<ReactComponent>]
    static member Documentation () =
        Html.div [
            StyledComponents.Navbar
                "Feliz.ReactHamburger"
                { Name = "nuget"; Link = "https://www-1.nuget.org/packages/Feliz.ReactHamburger/" }
                { Name = "npm"; Link = "https://www.npmjs.com/package/hamburger-react" }
            Components.Demo()
        ]

