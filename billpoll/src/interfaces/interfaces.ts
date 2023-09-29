export interface Poll {
  id: string;
  name: string;
}

export interface OptionCreate {
  name: string;
}

export interface PollCreate {
  name: string;
  options: OptionCreate[];
}
