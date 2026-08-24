# Receipt Processing Engine | C# .NET 8

A refactored receipt processor that applies discounts, taxes, and formatting rules using  design patterns.

## Problem
The original implementation mixed concerns: CLI, I/O, parsing, business rules, and formatting were all in one place. 
Goal: Improve maintainability, testability, and extensibility for multi-country tax rules.

## Solution / Architecture
-
- **Rules Engine**: `Applicable` + `Priority` + `Resolver` pattern to select country-specific tax/discount rules
- **Patterns Used**: Builder
- **Testing**: Characterization tests to ensure behavior-preserving refactors

## How to Run
1. `dotnet run -- input.csv`
2. Reads CSV basket → applies rules → outputs formatted receipt.txt

## What I Learned
Refactoring legacy code without breaking behavior, applying SOLID, and designing for future requirements like new tax jurisdictions.
