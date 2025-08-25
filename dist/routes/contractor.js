import { Router } from "express";
import { prisma } from "../prisma.js";
import { authenticate, requireRole } from "../middleware/auth.js";
import { z } from "zod";
const router = Router();
router.use(authenticate, requireRole(["CONTRACTOR"]));
// List open projects without bills
router.get("/projects/open", async (_req, res) => {
    const projects = await prisma.project.findMany({
        where: { status: "OPEN", bill: null },
        include: { user: { select: { id: true, name: true } } }
    });
    res.json(projects);
});
const createBillSchema = z.object({
    projectId: z.number().int().positive(),
    items: z.array(z.object({ description: z.string(), amountCents: z.number().int().nonnegative() })).min(1)
});
router.post("/bills", async (req, res) => {
    const parsed = createBillSchema.safeParse(req.body);
    if (!parsed.success)
        return res.status(400).json({ error: parsed.error.flatten() });
    const { projectId, items } = parsed.data;
    const { userId } = req.user;
    const project = await prisma.project.findUnique({ where: { id: projectId } });
    if (!project)
        return res.status(404).json({ error: "Project not found" });
    if (project.status !== "OPEN")
        return res.status(400).json({ error: "Project not open" });
    const totalAmountCents = items.reduce((sum, i) => sum + i.amountCents, 0);
    const bill = await prisma.bill.create({
        data: {
            projectId,
            contractorId: userId,
            items: items,
            totalAmountCents
        }
    });
    await prisma.project.update({ where: { id: projectId }, data: { status: "BILLED" } });
    res.json(bill);
});
// List my bills
router.get("/bills", async (req, res) => {
    const { userId } = req.user;
    const bills = await prisma.bill.findMany({ where: { contractorId: userId }, include: { project: true } });
    res.json(bills);
});
export default router;
