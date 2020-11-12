import pygame
import pygame.gfxdraw
import graphics

class Score():
    def __init__(self, name, size, points):
        """Eltárolja egy játékos nevét, pontszámát és a pálya méretét amin játszott."""
        self.name = name
        self.size = size
        self.points = points

def scoreboard_txt_rt():
    """Beolvassa a ranglistát a Scoreboard txt fájlból."""
    data = []
    with open('Scoreboard.txt') as f:
        for line in f:
            line = line.rstrip("\n")
            details = line.split("\t")
            data.append(Score(details[0], details[1], details[2]))
    return data

def scoreboard(window):
    """Scoreboard főfüggvénye"""
    window.fill(pygame.Color('#000000'))
    graphics.title_draw(window, 'Scoreboard', 86, pygame.Color('#FFFFFF'), 80)
    data = scoreboard_txt_rt()
    graphics.scoreboard_draw(window, data)
    pygame.display.update()

    while True:
        event = pygame.event.wait()
        if event.type == pygame.KEYDOWN:
            if event.key == pygame.K_ESCAPE:
                window.fill(pygame.Color('#000000'))
                break
        if event.type == pygame.QUIT:
            pygame.quit()
