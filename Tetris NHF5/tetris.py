import pygame
import pygame.gfxdraw
import random
import options
import graphics
import controls

class Counter():
    def __init__(self, x, y):
        """Eltárolja egy blokk a pályán lévő x és y koordinátáit."""
        self.x = x
        self.y = y

def board(w, h):
    """Létrehozza a pályát"""
    return [[0 for _ in range(w)] for _ in range(h)]

def random_block():
    """Kiválaszt egy véletlenszerű blokkot"""
    rnd = random.randint(1,7)
    if rnd == 1:
        return [[1,1],[1,1]] # O Blokk
    if rnd == 2:
        return [[0,0,1],[0,0,1],[0,1,1]] # J Blokk
    if rnd == 3:
        return [[1,0,0],[1,0,0],[1,1,0]] # L Blokk
    if rnd == 4:
        return [[0,1,0],[1,1,0],[1,0,0]] # Z Blokk
    if rnd == 5:
        return [[0,1,0],[0,1,1],[0,0,1]] # S Blokk
    if rnd == 6:
        return [[0,1,0,0],[0,1,0,0],[0,1,0,0],[0,1,0,0]] # I Blokk
    if rnd == 7:
        return [[0,0,0],[0,1,0],[1,1,1]] # T Blokk

def check_ycollision(block, board, counter):
    """Ellenőrzi, hogy az aktuális blokk alatt van-e objektum."""
    for i in range(len(block)):
        for j in range(len(block[i])):
            if block[i][j] != 0:
                if counter.y + i >= len(board)-1:
                    return True
                elif board[counter.y + i + 1][counter.x + j] == 1:
                    return True
    return False

def check_xcollision_l(block, board, counter):
    """Ellenőrzi, hogy az aktuális blokktól balra van-e objektum."""
    for i in range(len(block)):
        for j in range(len(block[i])):
            if block[i][j] != 0:
                if counter.x + j <= 0:
                    return True
                elif board[counter.y + i][counter.x + j - 1] == 1:
                    return True
    return False

def check_xcollision_r(settings, block, board, counter):
    """Ellenőrzi, hogy az aktuális blokktól jobbra van-e objektum."""
    for i in range(len(block)):
        for j in range(len(block[i])):
            if block[i][j] != 0:
                if counter.x + j >= settings.width-1:
                    return True
                elif board[counter.y + i][counter.x + j + 1] == 1:
                    return True
    return False

def block_to_board(block, board, counter):
    """Betölti az aktuális blokkot a pálya mátrixába xés y koordináták szerint"""
    for i in range(len(block)):
        for j in range(len(block[i])):
            if block[i][j] !=0:
                board[counter.y+i][counter.x+j] = block[i][j]
    return board

def block_transpose(block):
    """Transzponálja egy blokk mátrixát"""
    return [[block[j][i] for j in range(len(block))] for i in range(len(block[0]))]

def block_mirror(block):
    """Egy virtuális y tengelyen tükrözi a blokk mátrixát"""
    for x in range(len(block)):
        block[x] = list(reversed(block[x]))
    return block

def turn_clockwise(block):
    """Elforgatja a blokkot óramutatóval megegyező irányba."""
    block = block_transpose(block)
    block = block_mirror(block)
    move = pygame.mixer.Sound('Move.wav')
    pygame.mixer.Sound.play(move)
    return block

def turn_counterclockwise(block):
    """Elforgatja a blokkot óramutatóval ellentétes irányba."""
    block = block_mirror(block)
    block = block_transpose(block)
    move = pygame.mixer.Sound('Move.wav')
    pygame.mixer.Sound.play(move)
    return block

def row_detect(settings, board, lvl, points):
    """Ellenőrzi van-e sikeresen feltöltött blokk. Kitörli majd a pálya
    legtetejére üres sort helyez. Kiszámolja a pontokat."""
    lines = 0
    bonus_points = 0
    for i in range(len(board)):
        success = True
        for j in range(len(board[i])):
            if board[i][j] == 0:
                success = False
        if success:
            lines += 1
            board.remove(board[i])
            board.insert(0, [0 for _ in range(settings.width)])
    bonus_points = points_for_rows(lines,lvl, points)
    return lines, board, points

def points_for_blocks(block, lvl):
    """Kiszámolja a blokk letételéért járó pontszámot."""
    bonus_points = 0
    size = 0
    for i in range(len(block)):
        for j in range(len(block[i])):
            if block[i][j] == 1:
                size += 1
    bonus_points = size * (lvl+1)
    return bonus_points

def points_for_rows(lines, lvl, points):
    """Kiszámolja a sikeres sorokért járó pontszámot."""
    if lines == 1:
        return ((lvl+1) * 100) + points
    if lines == 2:
        return (2*(lvl+1) * 100) + points
    if lines == 3:
        return (4*(lvl+1) * 100) + points
    if lines == 4:
        return (7*(lvl+1) * 100) + points

def tetris(window):
    """Tetris főfüggvény"""
    settings = options.settings_rt()
    game_board = board(settings.width, settings.height)
    next_block = random_block()
    current_block = None
    lines = 0
    points = 0
    counter = Counter(settings.width // 2 - 1, 0)
    pygame.mixer.music.load('Music.mp3')
    pygame.mixer.music.play(-1)
    while True:
        if 1 in game_board[0] :
            window.fill(pygame.Color('#000000'))
            break
        pygame.time.set_timer(pygame.USEREVENT, 1000 - 30*(settings.lvl + lines // 10))
        if current_block == None:
            current_block = next_block
            next_block = random_block()
            graphics.tetris_screen_update(window, settings, game_board, current_block, counter, (settings.lvl + lines // 10), lines)
        event = pygame.event.wait()
        if event.type == pygame.USEREVENT:
            if check_ycollision(current_block, game_board, counter):
                game_board = block_to_board(current_block, game_board, counter)
                line, game_board, points = row_detect(settings, game_board, (settings.lvl + lines // 10), points)
                points_for_blocks(current_block, (settings.lvl + lines // 10))
                current_block = None
                counter = Counter(settings.width // 2 - 1, 0)
            else:
                counter.y += 1
                graphics.tetris_screen_update(window, settings, game_board, current_block, counter, (settings.lvl + lines // 10), lines)
        if event.type == pygame.KEYDOWN:
            if event.key == pygame.K_LEFT:
                counter = controls.tetris_K_LEFT(window, settings, current_block, game_board, counter, (settings.lvl + lines // 10), lines)
            if event.key == pygame.K_RIGHT:
                counter = controls.tetris_K_RIGHT(window, settings, current_block, game_board, counter, (settings.lvl + lines // 10), lines)
            if event.key == pygame.K_DOWN:
                if check_ycollision(current_block, game_board, counter):
                    game_board = block_to_board(current_block, game_board, counter)
                    line, game_board, points = row_detect(settings, game_board,(settings.lvl + lines // 10), points)
                    points_for_blocks(current_block, settings.lvl + lines // 10)
                    current_block = None
                    counter = Counter(settings.width // 2 - 1, 0)                    
                else:
                    counter.y += 1
                    graphics.tetris_screen_update(window, settings, game_board, current_block, counter, (settings.lvl + lines // 10), lines)
            if event.key == pygame.K_a:
                if check_xcollision_r(settings, current_block, game_board, counter) or check_xcollision_l(current_block, game_board, counter) or check_ycollision(current_block, game_board, counter):
                    pass
                else:
                    current_block = turn_counterclockwise(current_block)
                    graphics.tetris_screen_update(window, settings, game_board, current_block, counter, (settings.lvl + lines // 10), lines)
            if event.key == pygame.K_d:
                if check_xcollision_r(settings, current_block, game_board, counter) or check_xcollision_l(current_block, game_board, counter) or check_ycollision(current_block, game_board, counter):
                    pass
                else:
                    current_block = turn_clockwise(current_block)
                    graphics.tetris_screen_update(window, settings, game_board, current_block, counter, (settings.lvl + lines // 10), lines)
            if event.key == pygame.K_ESCAPE:
                window.fill(pygame.Color('#000000'))
                break
        if event.type == pygame.QUIT:
            pygame.quit()
