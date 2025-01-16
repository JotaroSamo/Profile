export interface CreatePost {
    title: string; // обязательное свойство
    content?: string; // необязательное свойство
    tags?: string[]; // необязательное свойство (массив строк)
}
