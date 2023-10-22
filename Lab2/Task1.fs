module Task1

let findMax number =
    let digits = [
        for ch in string(number).ToCharArray() do
            if (int(ch) >= 48 && int(ch) <= 57) then
                yield int(string(ch))
    ]
    
    List.max(digits)

let run =
    printfn "Задание 1:\nПолучить список из максимальных цифр натуральных чисел, содержащихся в исходном списке.\n"

    printfn "Введите числа через пробел:"
    let userInput = System.Console.ReadLine()
    
    if (userInput.Length = 0) then
        printfn "Результат: []"
    else
        let numbers = [ for arg in userInput.Split(' ') -> int64(arg) ]
        let result = List.map ( findMax ) numbers
        printfn "Результат: %A" result
    0