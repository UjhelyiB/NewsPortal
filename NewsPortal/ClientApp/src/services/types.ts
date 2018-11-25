export interface News {
    id: number;
    title: string;
    cover: any; // TODO
    intro: string;
    body: string[];
    tags: string; // TODO
    date: string;
    author: string;
    categoryIds: number[];
}

export interface Category {
    id: number;
    title: string;
}
