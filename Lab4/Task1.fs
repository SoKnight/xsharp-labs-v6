module Task1

type Tree =
    | Node of string * List<Tree>
    | Empty

// проходит по контенту и веткам дерева, форматируя его в читабельный вид
let rec walkTreeToPrint (prefix: string) (tree: Tree): string =
    match tree with
    | Node (data: string, children: List<Tree>) ->
        let nextPrefix = prefix |> sprintf "%s   "
        let mappedChildren = children |> List.map (walkTreeToPrint nextPrefix)
        let childContent = mappedChildren |> String.concat "\n"

        if (childContent.Length <> 0) then
            sprintf "%s%s:\n%s" prefix data childContent
        else
            sprintf "%s%s" prefix data
    | Empty -> ""

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

    let tree: Tree = 
        Node("inventory", [
            Node("armor", [
                Node("helmet", []);
                Node("chestplate", []);
                Node("pants", []);
                Node("boots", [])
            ]);
            Node("weapons", [
                Node("sword", []);
                Node("shield", []);
                Node("bow", [])
            ]);
            Node("tools", [
                Node("pickaxe", []);
                Node("shovel", []);
                Node("axe", [])
            ])
        ])

    printfn "[Исходное дерево]\n%s" (walkTreeToPrint "" tree)
    printfn "\n[Результат]\n%s" (walkTreeToPrint "" (modifyTree tree))
    0