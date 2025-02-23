import { createSlice, PayloadAction } from "@reduxjs/toolkit"

export interface UserState {
  username: string
}

const initialState: UserState = {
  username: "", // По умолчанию пустая строка
}

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    setUsername: (state, action: PayloadAction<string>) => {
      state.username = action.payload
    },
  },
})

export const { setUsername } = userSlice.actions
export default userSlice.reducer
