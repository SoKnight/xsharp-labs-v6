module Task1

open System

type Tree =
    | Node of string * List<Tree>
    | Empty

let random = Random()

// генерирует случайную строку в виде последовательности цифр
let generateRandomStr = 
    let chars = "0123456789"
    let charsLen = chars.Length

    fun len -> 
        let randomChars = [|for i in 0..len -> chars.[random.Next(charsLen)]|]
        new String(randomChars)

// генерирует дерево со случайным количеством дочерних узлов
let rec generateTree (data: string, childChance: double): Tree =
    let children = [
        for i in 1.0 .. 5.0 do
            if (random.NextDouble() < childChance ** i) then
                yield generateTree(generateRandomStr(5) |> sprintf "child-%s", childChance ** i);
    ]

    Node(data, children);

// pretty-printer для деревьев :o
let rec walkTreeToPrint (prefix: string) (tree: Tree): string =
    match tree with
    | Node (data: string, children: List<Tree>) ->
        let nextPrefix = prefix |> sprintf "%s   "
        let mappedChildren = children |> List.map (walkTreeToPrint nextPrefix)
        let childContent = mappedChildren |> String.concat "\n"

        if (childContent.Length <> 0) then
            sprintf "%s'%s':\n%s" prefix data childContent
        else
            sprintf "%s'%s'" prefix data
    | Empty -> ""

// упрощённая функция для печати дерева в красивом виде
let printTree (tree: Tree): string =
    walkTreeToPrint "" tree

// изменяет строку так, как сказано в задании ('a' -> 'b'...)
let modifyDataString (data: string): string =
    if (data.Length <> 0) then
        data |> String.collect (fun ch -> string(char(int32(ch) + 1)))
    else
        data

// модифицирует исходное дерево путём вызовов #modifyDataString и последующей сборки нового дерева
let rec modifyTree (tree: Tree): Tree =
    match tree with
    | Node (data: string, children: List<Tree>) ->
        let modifiedData = modifyDataString data
        let modifiedChildren = children |> List.map modifyTree
        Node(modifiedData, modifiedChildren)
    | Empty -> Empty

let run =
    printfn "Задание 1:\nДерево содержит строки.\nЗаменить в каждой строке каждый символ на следующий по порядку.\n"

    //let tree: Tree =
    //    Node("one node", [])

    //let tree: Tree =
    //    Node("1st", [
    //        Node("2nd", [
    //            Node("3rd", [
    //                Node("4th", [
    //                    Node("5th", [])
    //                ])
    //            ])
    //        ])
    //    ])

    //let tree: Tree = 
    //    Node("inventory", [
    //        Node("armor", [
    //            Node("helmet", []);
    //            Node("chestplate", []);
    //            Node("pants", []);
    //            Node("boots", [])
    //        ]);
    //        Node("weapons", [
    //            Node("sword", []);
    //            Node("shield", []);
    //            Node("bow", [])
    //        ]);
    //        Node("tools", [
    //            Node("pickaxe", []);
    //            Node("shovel", []);
    //            Node("axe", [])
    //        ])
    //    ])

    let tree: Tree =
        generateTree("root", 0.6)

    printfn "[Исходное дерево]\n%s" (printTree tree)
    printfn "\n[Результат]\n%s" (printTree (modifyTree tree))
    0