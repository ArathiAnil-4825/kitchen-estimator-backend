import "dotenv/config";
import express from "express";
import cors from "cors";
import authRoutes from "./routes/auth.js";
import userRoutes from "./routes/user.js";
import contractorRoutes from "./routes/contractor.js";
import adminRoutes from "./routes/admin.js";
const app = express();
app.use(cors());
app.use(express.json());
app.get("/health", (_req, res) => res.json({ ok: true }));
app.use("/auth", authRoutes);
app.use("/user", userRoutes);
app.use("/contractor", contractorRoutes);
app.use("/admin", adminRoutes);
const port = process.env.PORT || 3000;
app.listen(port, () => {
    console.log(`Server running on http://localhost:${port}`);
});
