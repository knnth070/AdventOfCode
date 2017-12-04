module Day4

    open System.IO
    open System

    let canonicalize word =
        word
        |> Seq.sort
        |> Array.ofSeq
        |> System.String

    let isValidPassphrase f (phrase:string) =
        phrase.Split(' ')
        |> Seq.groupBy f
        |> Seq.filter (fun (_, l) -> Seq.length l <> 1)
        |> Seq.isEmpty

    let validPassphrases f input =
        input
        |> Seq.filter (isValidPassphrase f)
        |> Seq.length

    let solve() =
        let input = File.ReadAllLines("..\..\Input\day4.txt")
        printfn "part 1 = %A" (validPassphrases id input)
        printfn "part 2 = %A" (validPassphrases canonicalize input)
        ()
