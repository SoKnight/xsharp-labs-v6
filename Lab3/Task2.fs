module Task2

let sumLengths seq = Seq.fold ( fun acc (word: string) -> acc + word.Length ) 0 seq

let run =
    printfn "Задание 2:\nПоследовательность содержит строки.\nНайти суммарную длину этих строк.\n"

    printfn "Введите слова через пробел: "
    let userInput = System.Console.ReadLine()

    if (userInput.Length = 0) then
        printfn "Сумма длин строк: 0"
    else
        let words = [ for str in userInput.Split(' ') -> str ]
        printfn "Сумма длин строк: %i" (sumLengths words)
    0