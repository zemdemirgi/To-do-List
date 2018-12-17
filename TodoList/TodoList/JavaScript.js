var Todo = /** @class */ (function () {
    function Todo(name, time, completed) {
        this.name = name;
        this.time = time;
        this.completed = completed;
    }
    return Todo;
}());
var TodoList = /** @class */ (function () {
    function TodoList() {
    }
    TodoList.prototype.createTodoItem = function (name, time) {
        var newItem = new Todo(name, time, false);
        var totalCount = TodoList.allTodos.push(newItem);
        return totalCount;
    };
    TodoList.prototype.allTodoItems = function () {
        return TodoList.allTodos;
    };
    TodoList.allTodos = new Array;
    return TodoList;
}());
window.onload = function () {
    var task = document.getElementById("todoName");
    var time = document.getElementById("todoTime");
    document.getElementById("add").addEventListener('click', function () { return toAlltask(task.value, time.value); });
};
function toAlltask(task, time) {
    var todo = new TodoList();
    todo.createTodoItem(task, time);
    var div = document.getElementById("todoList");
    var list = "<dl class='text'>";
    for (var index = 0; index < TodoList.allTodos.length; index++) {
        list = list + " <dt> " + TodoList.allTodos[index].name + " (" + TodoList.allTodos[index].time + ") ";
    }
    list += "</dl>";
    div.innerHTML = list;
    document.getElementById("todoName").value = "";
    document.getElementById("todoTime").value = "";
}
