import { Likes } from "./likes";
import { Tags } from "./tags";
import { User } from "./user"

export interface Post {
    postId: number;
    userId: number;
    title: string;
    desc?: string;
    likes: number;
    date: Date;
    User: User;
}