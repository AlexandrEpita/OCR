
#ifndef NAME_OF_THE_HEADER_FILE
#define NAME_OF_THE_HEADER_FILE
struct sudoku { 
    int grille[][];//matrice de notre sudoku
    char s[];//contenu du tx
};
struct sudoku load_grille(char path[]);// txt-> sudoku.s[]
void update_grille(struct sudoku sud);//sudoku.s[] ->sudoku.grille[]
void update_string(struct sudoku sud);//sudoku.grille[] -> sudoku.s[]
int ouverture_fichier(struct sudoku sud, char nom[]);//sudoku ->txt

void solver(struct sudoku sud);
#endif