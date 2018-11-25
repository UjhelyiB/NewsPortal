export interface News {
    Id: number;
    Title: string;
    Cover: any; // TODO
    Intro: string;
    Body: string[];
    Tags: string; // TODO
    Date: string;
    Author: string;
    CategoryIds: number[];
}

export interface Category {
    id: number;
    title: string;
}
