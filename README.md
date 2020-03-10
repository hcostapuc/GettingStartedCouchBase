# GettingStartedCouchBase
POC utilizando uma API C# com couchbase

## Para executarmos a API precisamos configurar o ambiente, segue os passos abaixo:

### 1 - Rodar o container do couchbase:

```
docker run -d --name db -p 8091-8094:8091-8094 -p 11210:11210 couchbase
```

### 2 - Acessar o couchbase:

```
http://localhost:8091
```

### 3 - Depois que fizer as configurações iniciais como o user/password do admin, alocação de memória para os services, criamos o bucket chamado "demo"

### 4 - Na aba "query" executamos o indice para que possamos utilizar o n1ql
```
CREATE INDEX ix_type ON demo(type)
```
### 5 - Agora só executar a aplicação!
