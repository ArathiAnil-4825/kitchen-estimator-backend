import { Router } from "express";
import { prisma } from "../prisma.js";
import { authenticate, requireRole } from "../middleware/auth.js";
import { z } from "zod";

const router = Router();

router.use(authenticate, requireRole(["USER"]));

const createProjectSchema = z.object({
  type: z.enum(["KITCHEN_CONSTRUCTION", "KITCHEN_RENOVATION"]),
  areaSqFt: z.number().int().positive()
});

router.post("/projects", async (req, res) => {
  const parsed = createProjectSchema.safeParse(req.body);
  if (!parsed.success) return res.status(400).json({ error: parsed.error.flatten() });
  const { type, areaSqFt } = parsed.data;
  const { userId } = (req as any).user as { userId: number };
  const project = await prisma.project.create({ data: { userId, type: type as any, areaSqFt } });
  res.json(project);
});

router.get("/projects", async (req, res) => {
  const { userId } = (req as any).user as { userId: number };
  const projects = await prisma.project.findMany({ where: { userId }, include: { bill: true } });
  res.json(projects);
});

export default router;
