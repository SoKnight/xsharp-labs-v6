module Task2

let run =
    printfn "Задание 2:\nНайти количество цифр в записи натурального числа.\n"

    printf "Введите число: "
    let userInput = System.Console.ReadLine()

    if (userInput.Length = 0) then
        printfn "Вам необходимо ввести какое-то натуральное число!"
    else
        let digits = [
            for ch in string(int64(userInput)).ToCharArray() do
                if (int(ch) >= 48 && int(ch) <= 57) then
                    yield ch
        ]
    
        printfn "Количество цифр в числе: %i" digits.Length
    0