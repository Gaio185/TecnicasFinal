# TecnicasFinal

Joao Gaio 25962; Ricardo Almeida n26344; Goncalo Gaspar n26531

Game1:

A classe Game1 é a classe principal do jogo que herda da classe Game, que é fornecida pelo XNA/MonoGame. Essa classe é responsável por gerenciar o ciclo de vida do jogo e conter os principais componentes do jogo.

Os atributos GraphicsDeviceManager, SpriteBatch e SpriteFont são usados para gerenciar o dispositivo gráfico, desenhar gráficos na tela e carregar uma fonte de texto, respectivamente.

As listas slimes, heavyslime e balls são usadas para armazenar instâncias de slimes, slimes pesados e bolas presentes no jogo.

O atributo attackRange define a distância máxima em que o jogador pode atacar os inimigos.

O atributo elapsedTime é usado para registrar o tempo decorrido desde o início do jogo.

As constantes Width e Height definem a largura e altura da janela do jogo.

Os atributos playerStartX e playerStartY são usados para definir a posição inicial do jogador.

Os atributos slime, heavySlime e ball são instâncias dos personagens do jogo (slimes, slimes pesados e bolas).

Os atributos spawnTimer, spawnTimer2 e spawnTimer3 são usados para controlar o tempo de spawn de novos inimigos (slimes, slimes pesados e bolas).

O atributo inGame indica se o jogo está em andamento ou não.

O atributo bestTime armazena o melhor tempo alcançado pelo jogador.

O atributo player é uma instância do personagem controlado pelo jogador.

O atributo background é uma textura que representa o fundo do jogo.

Os atributos _song, _hurtSound, _attackSound, _slimeHurtSound, _heavySlimeHurtSound e _ballHurtSound são instâncias de efeitos sonoros usados no jogo.

Os atributos _attackSoundInstance e _hurtSoundInstance são instâncias de efeitos sonoros que são reproduzidos durante o jogo.

O construtor da classe Game1 é responsável por inicializar os componentes principais do jogo, como o gerenciador de gráficos e o diretório de conteúdo.

O método Initialize é chamado uma vez no início do jogo e é usado para inicializar os valores iniciais das variáveis e instanciar os objetos necessários.

O método LoadContent é chamado no início do jogo para carregar os recursos gráficos e sonoros necessários.

O método Update é chamado a cada quadro do jogo e é usado para atualizar a lógica do jogo, como verificar entrada do teclado, atualizar a posição dos personagens e verificar colisões.

O método Update é chamado a cada quadro do jogo e é usado para atualizar a lógica do jogo, como verificar entrada do teclado, atualizar a posição dos personagens e verificar colisões.

O método Draw é responsável por desenhar os elementos gráficos na tela a cada quadro. Ele usa o objeto SpriteBatch para desenhar as texturas dos personagens, do fundo e as informações do jogo.

O método ResetGame é chamado para reiniciar o jogo quando o jogador perde. Ele redefine as variáveis e os personagens para seus valores iniciais.

O método CheckCollisions verifica se houve colisões entre o jogador e os inimigos. Se ocorrer uma colisão, o jogador é ferido e o jogo é reiniciado.

O método SpawnSlime é usado para criar uma nova instância de slime e adicioná-la à lista de slimes.

O método SpawnHeavySlime é usado para criar uma nova instância de slime pesado e adicioná-la à lista de slimes pesados.

O método SpawnBall é usado para criar uma nova instância de bola e adicioná-la à lista de bolas.

O método PlayAttackSound é usado para reproduzir o som de ataque do jogador.

O método PlayHurtSound é usado para reproduzir o som de ferimento do jogador.

O método PlaySlimeHurtSound é usado para reproduzir o som de ferimento do slime.

O método PlayHeavySlimeHurtSound é usado para reproduzir o som de ferimento do slime pesado.

O método PlayBallHurtSound é usado para reproduzir o som de ferimento da bola.


//---------------------------------------------------------------
Player:

Este código define uma classe chamada Player e uma enumeraçao(enum) chamada Direction.

A enumeração Direction define as direções possíveis em que o jogador pode se mover: Up, Down, Left e Right.

A classe Player representa o jogador no jogo e possui os seguintes membros:

defaultPosition: uma posição padrão para o jogador, representada por um objeto Vector2 com coordenadas X e Y.
position: a posição atual do jogador, representada por um objeto Vector2.
direction: a direção atual do jogador, do tipo Direction.
maxHP: a quantidade máxima de pontos de vida do jogador, representada por um valor de ponto flutuante.
currentHP: a quantidade atual de pontos de vida do jogador, representada por um valor de ponto flutuante.
speed: a velocidade do jogador em pixels por segundo.
radius: o raio do jogador, usado para verificar colisões com outros objetos.
game: uma referência ao objeto Game1, que representa o jogo.
animationFrames: uma lista de texturas que representam os quadros da animação do jogador.
currentFrame: o índice do quadro atual da animação.
frameTime: o tempo entre cada quadro da animação.
elapsedTime: o tempo decorrido desde o último quadro da animação.
O construtor da classe Player inicializa as propriedades e carrega as texturas dos quadros da animação na lista animationFrames.

O método Update é responsável por atualizar a posição do jogador com base nas teclas pressionadas pelo usuário. Ele verifica se as teclas de movimento estão pressionadas (setas direcionais ou teclas WASD) e atualiza a posição do jogador na direção correspondente, desde que a posição resultante esteja dentro dos limites do jogo. Além disso, ele atualiza o quadro da animação com base no tempo decorrido.

O método Draw é responsável por desenhar o jogador na tela. Ele utiliza o objeto SpriteBatch para desenhar o quadro atual da animação na posição atual do jogador.

Essa classe representa um jogador controlado pelo teclado em um jogo 2D. Ele pode se mover nas direções especificadas e exibe uma animação enquanto se move.

//--------------------------
As classes Slime, HeavySlime, Ball possuem a mesma estrutura
Falaremos da classe Slime


A classe Slime representa um slime no jogo. Vou explicar os principais elementos e métodos dessa classe:

Propriedade Texture: Armazena a textura do slime, que será usada para desenhá-lo na tela.

Variáveis privadas position e direction: Armazenam a posição e a direção do slime.

Propriedade Radius: Define e obtém o raio do slime.

Propriedade Bounds: Obtém um retângulo delimitador em torno do slime, com base em sua posição e raio.

Variáveis privadas game, slimeTextures, speed, damage, player, animationFrames, currentFrame, frameTime e elapsedTime: Armazenam informações sobre o jogo, velocidade do slime, dano causado, jogador alvo, quadros de animação, índice do quadro atual, tempo entre quadros da animação e tempo decorrido.

Propriedade Position: Define e obtém a posição do slime.

Propriedade Damage: Obtém o dano causado pelo slime.

Método construtor Slime: Inicializa uma nova instância da classe Slime. Recebe como parâmetros uma instância do jogo Game1, o jogador alvo targetPlayer e a posição inicial positionX do slime. Carrega as texturas dos quadros da animação do slime.

Método Update: Atualiza o slime a cada quadro do jogo. Recebe como parâmetro o tempo de jogo gameTime. Calcula a direção em direção ao jogador alvo, atualiza a posição com base na direção e velocidade, verifica se o slime ultrapassou as bordas da janela e ajusta a posição se necessário. Atualiza a animação do slime.

Método Draw: Desenha o slime na tela usando o objeto SpriteBatch. Desenha o quadro atual da animação do slime na posição especificada.