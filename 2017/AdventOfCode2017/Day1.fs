module Day1

    open System.IO

    let captcha (input:string) skipCount =
        let offset = Seq.append (Seq.skip skipCount input) input

        Seq.zip input offset
        |> Seq.fold (fun acc (i, o) -> acc + (if i = o then int i - int '0' else 0)) 0

    let solve() =
        let input = File.ReadAllLines("..\..\Input\day1.txt").[0]
        printfn "part 1 = %A" <| captcha input 1
        printfn "part 2 = %A" <| captcha input (input.Length / 2)
