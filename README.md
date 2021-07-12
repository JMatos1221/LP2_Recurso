# Projeto Recurso LP2  
Autoria:  
- André Figueira - 21901435  
  - Swap and Reproduction;
  - General Bugfixing;
  - Controller Interface;
  - Console View;
  - Input in Console;
  - Unity Project View;
  - Part of Delivery Report;  
  <br>
- Hugo Carvalho - 21901624  
  - Grid Filling;
  - Generate Positions and Method to Define Pieces Position;
  - Grid Properties;
  - Missing Comments;
  - Console Controller;
  - Unity Setup;
  - General Bugfixing;
  - Part of Delivery Report;  
  <br>
- João Matos - 21901219
  - Piece Enum and Grid Class;
  - Poisson Class;
  - Selection;
  - General Bugfixing;
  - Grid size;
  - Submodules;
  - Common Reference;
  - Command line arguments and Errors;
  - Controller;
  - More Bugfixing;   
  - Part of Delivery Report;

## Sumário  
Para o [projeto de recurso de Linguagens de Programação II](https://github.com/VideojogosLusofona/lp2_2020_prec) foi proposto realizar uma simulação do jogo `Pedra, Papel, Tesoura` num ecossistema em que competem pelo seu espaço no mundo em Consola e Unity.  
Este projeto foi realizado com base na máteria de LP2, principalmente nos Design Patterns.  

## Arquitetura da Solucao  
Para a solução do nosso projeto, utilizámos o `Model View Controller` e o `Iterator Pattern`, separando assim os dados do programa da lógica e loop e também da `GUI`.  
O projeto foi organizado com submódulos, sendo que existe código comum para o projeto em Consola e para o projeto em Unity, onde esse código comum contém o `Model` do Pattern `Model View Controller`, sendo a class `Grid.cs` que faz uso de outras classes como `Piece.cs` que apenas contém uma `enum` para a classificação de cada "casa" na simulação. Temos depois a classe `Poisson.cs` que para cada evento `Swap`, `Reproduction` e `Selection`, vai gerar um número que ccorresponderá à quantidade de vezes que esse evento vai ocorrer na frame atual. Por fim temos a interface `IController` que é implementada no `Controller` em ambos os projetos de Consola e Unity.

### Consola  
Para o projeto em consola foram utilizadas 3 classes, `ConsoleView.cs`, `ConsoleController.cs` e `Program.cs` para correr o projeto, temos também uma interface `IConsoleView` de modo a dependermos de uma abstração.  
Correndo o programa é feita a leitura de argumentos e caso não tenham sido passados argumentos suficientes ou tenham sido passados argumentos a mais, o programa gera uma exceção e termina seguidamente, o mesmo acontece se o formato dos argumentos não corresponder ou tiver fora do alcance pretendido.  
Caso esteja tudo correto e não ocorra nenhum destes erros, o programa prossegue e instancia uma `Grid`, um `ConsoleController` e um `ConsoleView` e começa o seu loop com `ConsoleController.Run()`.

ConsoleController.Run():  
1. O programa começa por preencher e imprimir a grelha da simulação, entrando de seguida num loop `while(running)`.
2. Verificamos se o jogo está pausado, caso esteja, passamos para o ponto 6.
3. São gerados os eventos a ocorrer na frame atual, sendo o número de cada tipo de evento calculado e após adicionados à lista, embaralhados.
4. Para cada evento na lista de eventos, corremos o mesmo.
5. Atualizamos apenas as coordenadas alteradas na grelha de simulação.
6. Verificamos input do jogador, sendo que a tecla `Escape` define `running` como `false`, terminando o programa, e a tecla `Spacebar` define `paused` com o seu valor oposto, pausando ou resumindo a simulação. Caso a simulação não termine, voltamos para o ponto 2.  

![Console UML](Images/Console_UML.png)

### Unity  
Para o projeto em Unity foram também utilizadas 3 classes, `Controller.cs`, `InputLimiter.cs` e `View.cs` com as mesmas responsabilidades para `Controller.cs` e `View.cs`, sendo que `InputLimiter.cs` serviu apenas para limitar os valores como pedido no projeto.  
Correndo o projeto Unity, temos 5 campos para atribuição dos valores referentes à largura e altura da grelha (entre 2 e 500 inclusive) de simulação e à frequência de cada evento (entre -1 e 1 sendo sendo este um número real).  
Sendo estes valores obrigatórios e estando definidos, podemos dar início à simulação pressionando o botão Start.  
Quando pressionamos o botão Start, são verificados todos os campos, e caso não esteja nenhum vazio, são lidos os valores e dá-se início a uma Corrotina que é o loop da simulação.  
A Corrotina começa com a criação de uma nova grelha para a simulação com as dimensões introduzidas e de seguida a criação de uma textura, que será a representação visual da grelha de simulação estando aplicada a uma `RawImage`.  
A grelha de simulação é preenchida e atualizada no ecrã e entramos então no loop que vai gerar os eventos como na consola, correr cada evento e atualizar as posições alteradas, sendo que no fim da Corrotina, é feito um `yield return new WaitForSeconds(0.05f)` para haver um ligeiro delay entre frames, tornando a visualização mais apelativa, e voltando assim ao início do loop.  
Temos um botão Pause, Stop e Quit, onde podemos pausar ou resumir a simulação, parar a simulação e alterar os valores ou sair da aplicação, respetivamente.  

![Unity UML](Images/Unity_UML.png)

## Observações e Resultados  
- Indicar o que acontece ao colocar swap-rate-exp a 1.0 deixando as restantes rate-exp a zero.  
  - O valor do lambda vai ser superior para a swap-rate-exp, fazendo com que o número de eventos `Swap` gerado seja superior ao dos eventos `Reproduction` e `Selection`.
    
- Indicar o que acontece ao colocar swap-rate-exp a -1.0 deixando as restantes rate-exp a zero.  
  - Neste caso acontece o oposto, ou seja, o valor do lambda será inferior ao dos outros eventos, fazendo com que o número de eventos `Swap` gerado seja inferior ao dos eventos `Reproduction` e `Selection`.  
  
- É possível encontrar algum conjunto de parâmetros que resulte na extinção de uma das espécies? Quando uma espécie se extingue, o que acontece às outras duas?
  - Não, pois os eventos ocorrem em posições aleatórias, sendo que podem ocorrer em qualquer espécie existente e a grelha de simulação é preenchida aleatoriamente quando criada. Quando sobram duas espécies, é uma questão de tempo para ocorrerem eventos de seleção e reprodução suficientes para a espécie "mais forte" das duas (Pedra, Papel, Tesoura) ocupar a grelha de simulação toda.  

## Referências  
[Vídeo LP2 MVC Example](https://www.youtube.com/watch?v=_z_iRUjmvzE)  
[Poisson Distribution (Junhao, based on Knuth)](https://en.wikipedia.org/wiki/Poisson_distribution#Generating_Poisson-distributed_random_variables)  
[Fisher-Yates Shuffle](https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle)  
[Unity Documentation](https://docs.unity3d.com/Manual/index.html)