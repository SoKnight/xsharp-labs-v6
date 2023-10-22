module Task1

type Tree =
    | Node of string * Tree * Tree
    | Empty

let rec printTree0 (tree: Tree) =
    match tree with
    | Node (data, left, right) ->
        printf "%s ( " data
        printTree0 (left)
        printf " ) ( "
        printTree0 (right)
        printf " )"
    | Empty -> ()

let printTree (tree: Tree) =
    printTree0 tree
    printfn ""

let run =
    printfn "Задание 1:\nДерево содержит строки.\nЗаменить в каждой строке каждый символ на следующий по порядку.\n"

    let tree: Tree = 
        Node("root",
            Node ("left", 
                Node ("child1", Empty, Empty),
                Empty
            ),
            Node ("right",
                Node ("child2", Empty, Empty),
                Node ("child3", Empty, Empty)
            )
        )

    printfn "Исходное дерево:"
    printTree tree

    // how to use #map() here?..
    let result = tree

    printfn "\nРезультат:"
    printTree result
    0