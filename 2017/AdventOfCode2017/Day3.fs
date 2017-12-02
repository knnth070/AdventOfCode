module Day3

    type Direction = Right | Up | Left | Down
    type Side = First | Second
    type State = { direction: Direction; side: Side; steps: int; length: int }
    type Coordinate = { X: int; Y: int }
    type Square = { coordinate: Coordinate; number: int }

    let initialState = { direction = Right; side = First; steps = 0; length = 1 }
    let initialSquare = { coordinate = { X = 0; Y = 0 }; number = 1 }

    let nextDirection = function
        | Right -> Up
        | Up -> Left
        | Left -> Down
        | Down -> Right

    let nextSide = function
        | First -> Second
        | Second -> First

    let nextState state steps =
        if state.steps + steps >= state.length
        then
            {
                direction = nextDirection state.direction;
                side = nextSide state.side;
                steps = 0;
                length = state.length + if state.side = Second then 1 else 0
            }
        else { state with steps = state.steps + steps }

    let nextCoordinate state coordinate length =
        match state.direction with
        | Right -> { coordinate with X = coordinate.X + length }
        | Up -> { coordinate with Y = coordinate.Y - length }
        | Left -> { coordinate with X = coordinate.X - length }
        | Down -> { coordinate with Y = coordinate.Y + length }

    let manhattanDistance coordinate =
        abs(coordinate.X) + abs(coordinate.Y)

    let squareIsAdjacent centre candidate =
        abs(candidate.X - centre.X) <= 1 && abs(candidate.Y - centre.Y) <= 1

    let rec findSquare state current searchNumber =
        let nextSquare square length =
            let newCoordinate = nextCoordinate state square.coordinate length
            let newNumber = square.number + length
            { coordinate = newCoordinate; number = newNumber }

        if current.number + state.length >= searchNumber
        then nextSquare current (searchNumber - current.number)
        else
            let newState = nextState state state.length
            let newSquare = nextSquare current state.length
            findSquare newState newSquare searchNumber

    let rec findSummedValue squares state current searchNumber =
        let nextSquare square =
            let newCoordinate = nextCoordinate state square.coordinate 1
            let newNumber = squares
                            |> List.filter (fun c -> squareIsAdjacent newCoordinate c.coordinate)
                            |> List.sumBy (fun p -> p.number)
            { coordinate = newCoordinate; number = newNumber }

        if current.number > searchNumber
        then current.number
        else
            let newSquare = nextSquare current
            let newState = nextState state 1
            findSummedValue (newSquare::squares) newState newSquare searchNumber

    let solve() =
        let input = 325489
        let square = findSquare initialState initialSquare input
        printfn "part 1 = %A" <| manhattanDistance square.coordinate
        printfn "part 2 = %A" <| findSummedValue [initialSquare] initialState initialSquare input
