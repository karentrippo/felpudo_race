Felpudo's Race: O resgate da Fofura 
# Prática em Laboratório UNITY - Felpudo's Race: O resgate da Fofura
##  Identificação do Projeto
* **Jogo:** Felpudo's Race: O resgate da Fofura
* **Desenvolvedores:** Karen V. Trippo e Waldomiro A. da Silva
* **Plataforma/Engine:** Unity 6 (Ambiente 2D)
* **Entrega:** Laboratório de Jogos Digitais via Google Classroom
PlayUnity: https://play.unity.com/en/games/efaa84c0-6155-4cd9-a863-4add8235719e/felpudoracekarenwaldomiro
YouTube: https://youtu.be/F_H1CSeZKeM
HitHub: https://github.com/karentrippo/felpudo_race

## Conceito e Narrativa do Jogo
O herói Fofuxo, um pássaro destemido, parte em uma jornada épica com seu veículo, pronto para resgatar sua amada Fofura, sequestrada pelo temível vilão Uruca. O vilão a mantém presa no topo de uma montanha cercada por capangas e armadilhas. 
Durante o percurso da esquerda para a direita, Fofuxo enfrenta os inimigos Lesmões, enviados para destruir seu carro (o que o obriga a pular ou abaixar-se), e coleta frutas saudáveis espalhadas pela estrada para recuperar sua energia física.

## Mecânicas e Comandos (Controles via Teclado)
* **Seta para Cima:** Comando de Pulo (Desvia de lesmas e projéteis rentes ao asfalto).
* **Seta para Baixo:** Comando de Abaixar (Modifica e encolhe dinamicamente o Box Collider 2D do jogador pela metade para desviar de obstáculos na altura do muro).
* **Tecla Space:** Opção de Ataque Ativo (Arremessa projéteis de limão em linha reta em direção ao Boss Uruca).
* **Tecla R:** Reinício Instantâneo (Recarrega a cena atual do zero e reinicializa o motor de física e os gerenciadores a qualquer momento).

## Programação e Elementos Técnicos Implementados
### 1. Aplicação de Físicas e Movimentação
* **Player:** Controlado por um componente `Rigidbody2D` com rotação travada (`Freeze Rotation Z`), utilizando forças físicas verticais para o pulo e checagem de solo via `OnCollisionEnter2D`.
* **Cenário Dinâmico (Parallax):** Rolagem infinita de texturas 2D (`Mathf.Repeat`) que simula o movimento contínuo do carro, da esquerda para a direita, ao longo do cenário em alta velocidade.

### 2. Gerenciadores Inteligentes (Spawn System)
* **Gerador de Inimigos:** Uma única fábrica inteligente dita o ritmo do gameplay. Ela utiliza intervalos de tempo aleatórios (`UnityEngine.Random.Range`) e sorteia se o adversário surgirá em uma altura variável no asfalto ou em uma altura fixa no topo do muro, evitando sobreposições impossíveis.
* **Gerador de Frutas (Itens de Cura):** Cria limões em posições aéreas livres. As frutas possuem um script autônomo com um temporizador de autodestruição que instancia partículas de explosão caso o jogador perca a oportunidade de coletá-las.

### 3. Sistema de Combate e Comportamento do Boss (Uruca)
O jogo possui uma progressão rígida controlada por um cronômetro de **1 minuto (60 segundos)**:
* **Fase de Sobrevivência:** O jogador enfrenta obstáculos que surgem continuamente e coleta frutas para manter a barra de vida cheia.
* **Transição para o Chefe:** Ao zerar o cronômetro, os geradores comuns de capangas e de itens são desligados, e a pista é limpa por código. O cenário de fundo mantém o movimento para simular uma perseguição em alta velocidade.
* **Batalha contra o Uruca:** O Boss é instanciado na tela com uma barra de vida dedicada (`Slider_VidaBoss`) que exibe o texto numérico `100 / 100`. O Boss ataca ativamente, utilizando um sistema de repetição contínua (`InvokeRepeating`), cuspindo tomates estragados e corrompidos em direções alternadas (alto e baixo). 
* O projétil do jogador (`LimaoProjetil`) utiliza colisores do tipo `Is Trigger` combinados com um `Rigidbody2D` com gravidade zerada no Boss para registrar os impactos físicos e aplicar a redução à barra de vida roxa.

## Condições de Fim de Jogo 
* **Condição de Derrota do Player:** Implementada de forma necessária e suficiente. Se a barra de vida vermelha do Fofuxo chegar a `0 / 100` devido aos impactos com as lesmas do cenário ou tomates podres do Boss, o motor de física do jogo é congelado (`Time.timeScale = 0f`) e a tela de **Game Over** é exibida.
* **Condição de Vitória Bem Definida:** Ocorre no clímax da fase. Após sobreviver à primeira etapa, o jogador deve descarregar ataques com a tecla Space contra o Uruca. Quando a barra numérica roxa do chefe chega a zero, o objeto do vilão é destruído, as físicas são congeladas e a tela de **Vitória** aparece no Canvas de UI.
## Organização dos Assets na Cena
O projeto fez uso otimizado do pacote disponibilizado no repositório laboratorial (`assets-lab10-unity`), aplicando a correta disposição visual das camadas no componente `Sprite Renderer` através do gerenciamento de `Sorting Layers` e `Order in Layer`, garantindo que os projéteis, personagens e barras de interface operem sempre visíveis à frente das estruturas e casinhas de fundo.

## Questões respondidas:
1.	Existe uma Condição de Vitória bem definida para o Jogo?
Sim! Na atividade, diz que a vitória poderia ser por "tempo determinado e, por conseguinte, pelo final do jogo". Implementamos o cronômetro de 1 minuto que limpa a fase e invoca o Boss Uruca. A vitória definitiva ocorre quando o jogador ataca o chefe com os limões (teclado) e o destrói.
2.	Existem Condições de Derrota do Player?
Sim! O edital exige condições de derrota e de danos. Implementamos a barra de vida integrada; o Fofuxo sofre dano ao bater nas lesmas e ao ser atingido pelos tomates podres do Uruca, o que aciona a tela de Game Over.
3.	Os adversários estão bem caracterizados?
Sim! Separamos as funções de forma inteligente: as lesmas comuns atacam de forma cadenciada por baixo e pelo muro durante a fase, e o Boss Uruca ataca ativamente cuspindo projéteis de frutas estragadas em alturas variadas após 1 minuto.
4.	A Movimentação do Player está bem definida?
Sim! O Fofuxo possui movimentação completa baseada em física: pula usando a Seta para Cima para desviar dos inimigos terrestres e se abaixa, encolhendo o colisor, usando a Seta para Baixo para desviar das ameaças aéreas.
