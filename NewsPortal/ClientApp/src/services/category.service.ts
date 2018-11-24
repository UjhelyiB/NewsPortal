import { Injectable } from "@angular/core";
import { Category } from "./types";
import { CATEGORIES } from "./mock-categories";

@Injectable()
export class CategoryService {

    getMockCategories(): Category[] {
        return CATEGORIES;
    }
}
