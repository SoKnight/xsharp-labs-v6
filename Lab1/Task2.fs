module Task2

open System

let removeMinus (userInput: string) =
    if (userInput.StartsWith "-") then
         userInput.Substring 1
     else
         userInput

let rec countNextDigit (input: string, count: int): bool * int =
    let charCode = int(input[0])

    if (charCode >= 48 && charCode <= 57) then
        if (input.Length > 1) then
            countNextDigit (input.Substring 1, count + 1)
        else
            (true, count)
    else
        (false, count)

let tryCountDigits (input: string): bool * int =
    if (input.Length <> 0 && (not (input.StartsWith "0") || input.Length = 1)) then
        match countNextDigit (input, 1) with
        | true, count -> (true, count)
        | _ -> (false, 0)
    else
        (false, 0)

let run =
    printfn "Задание 2:\nНайти количество цифр в записи натурального числа.\n"

    printf "Введите число: "
    let userInput = System.Console.ReadLine()

    if (userInput.Length = 0) then
        printfn "Вам необходимо ввести какое-то натуральное число!"
    else
        let stripped = removeMinus userInput

        match tryCountDigits stripped with
        | (true, count) -> printfn "Количество цифр в числе: %i" count
        | _ -> printfn "Введённое значение не является числом!"
    0