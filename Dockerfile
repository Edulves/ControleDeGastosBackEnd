# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copia o projeto e restaura as dependências
COPY ["ExpensesControl.csproj", "./"]
RUN dotnet restore "ExpensesControl.csproj"

# Copia o restante dos arquivos
COPY . .

# Compila e publica a aplicação
RUN dotnet publish "ExpensesControl.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

# Copia apenas os arquivos publicados
COPY --from=build /app/publish .

# Porta utilizada pela aplicação
EXPOSE 8080

ENTRYPOINT ["dotnet", "ExpensesControl.dll"]

