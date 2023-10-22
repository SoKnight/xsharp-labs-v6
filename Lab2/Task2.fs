module Task2

let sumLengths list = List.fold ( fun acc (word: string) -> acc + word.Length ) 0 list

let run =
    printfn "Задание 2:\nСписок содержит строки.\nНайти суммарную длину этих строк.\n"

    printfn "Введите слова через пробел: "
    let userInput = System.Console.ReadLine()

    if (userInput.Length = 0) then
        printfn "Сумма длин строк: 0"
    else
        let words = [ for str in userInput.Split(' ') -> str ]
        printfn "Сумма длин строк: %i" (sumLengths words)
    0