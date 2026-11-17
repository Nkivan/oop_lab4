# Схеми до лабораторної роботи 4 - варіант 2

## 1. Діаграма класів

```mermaid
---
config:
  layout: elk
---
classDiagram
    direction TB

    class Program {
        -Main()
    }

    class ConsoleMenu {
        +Run()
        -CountSymbolInteractive()
        -CalculatorInteractive()
    }

    class DelegateDemo {
        <<static>>
        +CharCountOperation AnonymousMethod
        +CharCountOperation Lambda
        +Run()
    }

    class CharCountOperation {
        <<delegate>>
        +Invoke(text, symbol) int
    }

    class CalculatorDemo {
        <<static>>
        +Run()
    }

    class Calculator {
        +string Name
        +int Result
        +event IntegerDivision
        +Calculator(name, initialValue)
        +Add(value)
        +Subtract(value)
        +Multiply(value)
        +Divide(divisor)
        +Reset(value)
        #OnIntegerDivision(e)
    }

    class IntegerDivisionEventArgs {
        +int Dividend
        +int Divisor
        +int Quotient
        +int Remainder
        +bool HasRemainder
    }

    class DivisionConsoleHandler {
        +HandleIntegerDivision(sender, e)
    }

    class DivisionStatisticsHandler {
        +int Count
        +int WithRemainder
        +HandleIntegerDivision(sender, e)
        +Summary() string
    }

    class EventArgs

    Program ..> ConsoleMenu : створює і запускає
    ConsoleMenu ..> DelegateDemo : демонстрація делегата
    ConsoleMenu ..> CalculatorDemo : демонстрація події
    ConsoleMenu ..> Calculator : інтерактивна робота
    DelegateDemo ..> CharCountOperation : анонімний метод і лямбда
    CalculatorDemo ..> Calculator : створює і викликає операції
    CalculatorDemo ..> DivisionConsoleHandler : підписує обробник
    CalculatorDemo ..> DivisionStatisticsHandler : підписує обробник
    Calculator ..> IntegerDivisionEventArgs : створює при події
    DivisionConsoleHandler ..> IntegerDivisionEventArgs : читає дані події
    DivisionStatisticsHandler ..> IntegerDivisionEventArgs : читає дані події
    EventArgs <|-- IntegerDivisionEventArgs : успадковує
```

## 2. Частина 1: делегат, анонімний метод і лямбда-вираз

```mermaid
---
config:
  layout: elk
---
flowchart LR
    A["Рядок і символ: banana, a"] --> B{"Делегат CharCountOperation"}
    B -->|"AnonymousMethod(text, symbol)"| C["Анонімний метод: цикл for"]
    B -->|"Lambda(text, symbol)"| D["Лямбда-вираз: LINQ Count"]
    C --> E["3"]
    D --> F["3"]
```

Алгоритм обчислення (анонімний метод):

```mermaid
---
config:
  layout: elk
---
flowchart TD
    A(["Початок"]) --> B{"text = null?"}
    B -->|"так"| X["ArgumentNullException"]
    B -->|"ні"| C["count = 0, i = 0"]
    C --> D{"i < довжина рядка?"}
    D -->|"ні"| R(["Повернути count"])
    D -->|"так"| E{"text[i] = symbol?"}
    E -->|"так"| F["count = count + 1"]
    E -->|"ні"| G["i = i + 1"]
    F --> G
    G --> D
```

## 3. Частина 2: метод Divide і генерація події

```mermaid
---
config:
  layout: elk
---
flowchart TD
    A(["Divide(divisor)"]) --> B{"divisor = 0?"}
    B -->|"так"| X["DivideByZeroException"]
    B -->|"ні"| C["dividend = Result"]
    C --> D["quotient = dividend / divisor (checked)"]
    D --> E["remainder = dividend - quotient * divisor"]
    E --> F["Result = quotient"]
    F --> G["OnIntegerDivision(новий IntegerDivisionEventArgs)"]
    G --> H{"Є підписники події?"}
    H -->|"ні"| R(["Кінець: подію нікому не передано"])
    H -->|"так"| I["Invoke(this, e): виклик усіх обробників по черзі"]
    I --> R2(["Кінець"])
```

## 4. Взаємодія об'єктів під час події

```mermaid
sequenceDiagram
    participant Demo as CalculatorDemo
    participant Calc as Calculator
    participant Con as DivisionConsoleHandler
    participant Stat as DivisionStatisticsHandler

    Demo->>Calc: new Calculator("Калькулятор-1", 100)
    Demo->>Calc: IntegerDivision += console.HandleIntegerDivision
    Demo->>Calc: IntegerDivision += statistics.HandleIntegerDivision

    Demo->>Calc: Divide(7)
    Note over Calc: 100 / 7 = 14, остача 2
    Calc->>Calc: OnIntegerDivision(args)
    Calc->>Con: HandleIntegerDivision(this, args)
    Con-->>Demo: Console: повідомлення про ділення
    Calc->>Stat: HandleIntegerDivision(this, args)
    Note over Stat: Count = 1, WithRemainder = 1

    Demo->>Calc: IntegerDivision -= console.HandleIntegerDivision

    Demo->>Calc: Divide(2)
    Note over Calc: 14 / 2 = 7, без остачі
    Calc->>Calc: OnIntegerDivision(args)
    Calc->>Stat: HandleIntegerDivision(this, args)
    Note over Stat: Count = 2, повідомлення в консоль немає

    Demo->>Calc: Divide(0)
    Calc-->>Demo: DivideByZeroException (подія не генерується)
```
